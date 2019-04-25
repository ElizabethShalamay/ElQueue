using ElQueue.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElQueue.Orchestrator.TimeSlotOrchestrator
{
    public interface ITimeSlotOrchestrator
    {
        Task<TimeSlotBm> GetTimeSlotByIdAsync(int id);

        Task<IEnumerable<TimeSlotBm>> GetAllTimeSlotsForQueueAsync(int queueId);

        Task<bool> CheckIfTimeSlotExistsAsync(int id);

        Task<IEnumerable<TimeSlotBm>> AddTimeSlotsAsync(DateTime start, DateTime end, int duration, int queueId);

        Task ClearTimeSlotAsync(int id);

        Task SelectSlotAsync(int id, string userId);
    }
}
