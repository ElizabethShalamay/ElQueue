using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using ElQueue.BLL.Models;
using ElQueue.BLL.Services;
using ElQueue.DAL.Models;
using ElQueue.DAL.UnitOfWork;
using ElQueue.Orchestrator.Dtos;

namespace ElQueue.Orchestrator.QueueOrchestrator
{
    public class QueueOrchestrator : IQueueOrchestrator
    {
        private readonly IUnitOfWork _storage;
        private readonly IMapper _mapper;
        private readonly IQueueService _queueService;

        public QueueOrchestrator(IUnitOfWork unitOfWork, IMapper mapper, IQueueService queueService)
        {
            _storage = unitOfWork;
            _mapper = mapper;
            _queueService = queueService;
        }

        public async Task<bool> CheckIfQueueExistsAsync(int id)
        {
            return await _storage.Queues.ExistsAsync(id);
        }

        public async Task<QueueBm> CreateQueueAsync(NewQueueDto queueDto)
        {
            Guard.Argument(() => queueDto).NotNull();
            var queueWithSameNameExists = await _storage.Queues.ExistsAsync(queueDto.Name);

            var queueBm = CheckBusinessRulesAndCreateQueue(queueDto);

            return await _queueService.CreateQueueAsync(queueBm);
        }

        public async Task DeleteQueueAsync(int id)
        {
            await _storage.Queues.DeleteAsync(id);
            await _storage.SaveAsync();
        }

        public async Task<IEnumerable<QueueBm>> GetAllQueuesAsync()
        {
            var queues = await _storage.Queues.GetAsync();

            // TODO: consider move select out of orchestrator
            var queueBms = _mapper.Map<IEnumerable<QueueBm>>(queues);

            return queueBms;
        }

        public async Task<QueueBm> GetQueueByIdAsync(int id)
        {
            var queue = await _storage.Queues.GetAsync(id);

            // TODO: consider move select out of orchestrator
            var queueBm = _mapper.Map<QueueBm>(queue);

            return queueBm;
        }

        public async Task<IEnumerable<QueueBm>> GetQueuesByAccountIdAsync(int id)
        {
            var queues = await _storage.Queues.GetAsync(queue => queue.AccountId == id);

            // TODO: consider move select out of orchestrator
            var queueBms = _mapper.Map<IEnumerable<QueueBm>>(queues);

            return queueBms;
        }

        public async Task UpdateQueueAsync(QueueToUpdateDto queueDto)
        {
            Guard.Argument(() => queueDto).NotNull();

            var queueWithIdExists = await _storage.Queues.ExistsAsync(queueDto.Id);
            Guard.Argument(() => queueWithIdExists).True();

            var queueWithNameExists = await _storage.Queues.FreeQueueNameExists(queueDto.Id, queueDto.Name);
            Guard.Argument(() => queueWithNameExists).False();

            var queueBm = CheckBusinessRulesAndCreateQueue(queueDto);
            var queue = _mapper.Map<Queue>(queueBm);

            await _queueService.UpdateQueueAsync(queueBm);
        }

        private QueueBm CheckBusinessRulesAndCreateQueue(NewQueueDto queueDto)
        {
            Guard.Argument(() => queueDto.Name).NotNull();
            var datesAreCorrect = queueDto.StartTime < queueDto.EndTime;

            Guard.Argument(() => datesAreCorrect).True();
            Guard.Argument(() => queueDto.TimeSlotDuration).Positive();
            Guard.Argument(() => queueDto.TimeSlotNumber).Positive();
            return _mapper.Map<QueueBm>(queueDto);
        }
    }
}
