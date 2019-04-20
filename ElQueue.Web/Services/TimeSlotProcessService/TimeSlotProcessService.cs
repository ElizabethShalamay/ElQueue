using System.Linq;
using System.Threading.Tasks;
using Dawn;
using ElQueue.Orchestrator.Dtos;
using ElQueue.Orchestrator.TimeSlotOrchestrator;
using Microsoft.AspNetCore.Mvc;

namespace ElQueue.Web.Services.TimeSlotProcessService
{
    public class TimeSlotProcessService : ITimeSlotProcessService
    {
        private readonly ITimeSlotOrchestrator _timeSlotOrchestrator;

        public TimeSlotProcessService(ITimeSlotOrchestrator timeSlotOrchestrator)
        {
            _timeSlotOrchestrator = timeSlotOrchestrator;
        }

        public async Task<ActionResult> ProcessTimeSlotAdditionAsync(TimeSlotTemplate template)
        {
            Guard.Argument(() => template).NotNull();

            var result = await _timeSlotOrchestrator.AddTimeSlotsAsync(
               template.StartTime, template.EndTime, template.TimeSlotDuration, template.QueueId);

            return new CreatedAtRouteResult(new { Ids = result.Select(timeSlot => timeSlot.Id)}, result);
        }

        public async Task<ActionResult> ProcessTimeSlotSelectionAsync(int id, int userId)
        {
            Guard.Argument(() => id).Positive();

            await _timeSlotOrchestrator.SelectSlotAsync(id, userId);

            return new NoContentResult();
        }
    }
}
