using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElQueue.BLL.Models;
using ElQueue.DAL.Models;
using ElQueue.DAL.UnitOfWork;

namespace ElQueue.BLL.Services
{
    public class QueueService : IQueueService
    {
        private readonly IUnitOfWork _storage;
        private IMapper _mapper;

        public QueueService(IUnitOfWork unitOfWork)
        {
            _storage = unitOfWork;
        }

        public async Task<IEnumerable<Queue>> GetQueuesByAccount(int accountId)
        {
            return await _storage.Queues.GetAsync(queue => queue.AccountId == accountId);
        }

        public async Task<QueueBm> CreateQueueAsync(QueueBm queueBm)
        {
            var queue = new Queue
            {
                Name = queueBm.Name,
                AccountId = queueBm.AccountId,
                Address = queueBm.Address,
                IsActive = false
            };

            await _storage.Queues.CreateAsync(queue);
            await _storage.SaveAsync();
            return _mapper.Map<QueueBm>(queue);
        }

        public async Task UpdateQueueAsync(QueueBm queueBm)
        {
            var queue = new Queue
            {
                Id = queueBm.Id,
                Name = queueBm.Name,
                AccountId = queueBm.AccountId,
                Address = queueBm.Address,
                IsActive = false
            };

            _storage.Queues.Update(queue);

            await _storage.SaveAsync();
        }
    }
}
