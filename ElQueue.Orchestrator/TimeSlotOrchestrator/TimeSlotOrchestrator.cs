using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using ElQueue.BLL.Models;
using ElQueue.BLL.Services.TimeSlotService;
using ElQueue.DAL.Enums;
using ElQueue.DAL.UnitOfWork;

namespace ElQueue.Orchestrator.TimeSlotOrchestrator
{
    public class TimeSlotOrchestrator : ITimeSlotOrchestrator
    {
        private readonly IUnitOfWork _storage;
        private readonly IMapper _mapper;
        private readonly ITimeSlotService _timeSlotService;

        public TimeSlotOrchestrator(IUnitOfWork unitOfWork, IMapper mapper, ITimeSlotService timeSlotService)
        {
            _storage = unitOfWork;
            _mapper = mapper;
            _timeSlotService = timeSlotService;
        }

        public async Task<IEnumerable<TimeSlotBm>> AddTimeSlotsAsync(DateTime start, DateTime end, int duration, int queueId)
        {
            var correctTimeFormat = start.Ticks < end.Ticks && duration > 0;
            Guard.Argument(() => correctTimeFormat).True();

            var timeSlots = _timeSlotService.GetTimeSlots(start, end, duration, queueId);

            await _storage.TimeSlots.CreateAsync(timeSlots);
            await _storage.SaveAsync();

            return _mapper.Map<IEnumerable<TimeSlotBm>>(timeSlots);
        }

        public async Task<bool> CheckIfTimeSlotExistsAsync(int id)
        {
            return await _storage.TimeSlots.ExistsAsync(id);
        }

        public async Task ClearTimeSlotAsync(int queueId)
        {
            var queueWithIdExists = await _storage.Queues.ExistsAsync(queueId);
            Guard.Argument(() => queueWithIdExists).True();

            var timeSlots = (await _storage.TimeSlots.GetAsync(timeSlot => timeSlot.QueueId == queueId))
                .Select(timeSlot => timeSlot.Id);

            await _storage.TimeSlots.DeleteAsync(timeSlots);
            await _storage.SaveAsync();
        }

        public async Task<IEnumerable<TimeSlotBm>> GetAllTimeSlotsForQueueAsync(int queueId)
        {
            var queueWithIdExists = await _storage.Queues.ExistsAsync(queueId);
            Guard.Argument(() => queueWithIdExists).True();

            var timeSlots = await _storage.TimeSlots.GetAsync(timeSlot => timeSlot.QueueId == queueId);
            return _mapper.Map<IEnumerable<TimeSlotBm>>(timeSlots);
        }

        public async Task<TimeSlotBm> GetTimeSlotByIdAsync(int id)
        {
            var timeSlot = await _storage.TimeSlots.GetAsync(id);

            return _mapper.Map<TimeSlotBm>(timeSlot);
        }

        public async Task SelectSlotAsync(int id, string userId)
        {
            // TODO: add user id validation
            var timeSlot = await _storage.TimeSlots.GetAsync(id);

            timeSlot.UserId = userId;
            timeSlot.Status = QueueStatus.TakingTurn;

            _storage.TimeSlots.Update(timeSlot);
            await _storage.SaveAsync();
        }
    }
}
