using System.Collections.Generic;
using System.Threading.Tasks;
using ElQueue.BLL.Models;
using ElQueue.Orchestrator.Dtos;
using ElQueue.Orchestrator.QueueOrchestrator;
using ElQueue.Orchestrator.TimeSlotOrchestrator;
using ElQueue.Web.Services.TimeSlotProcessService;
using Microsoft.AspNetCore.Mvc;

namespace ElQueue.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly ITimeSlotOrchestrator _timeSlotOrchestrator;
        private readonly IQueueOrchestrator _queueOrchestrator;
        private readonly ITimeSlotProcessService _timeSlotProcessService;

        public TimeSlotsController(
            ITimeSlotOrchestrator timeSlotOrchestrator, 
            IQueueOrchestrator queueOrchestrator, 
            ITimeSlotProcessService timeSlotProcessService)
        {
            _timeSlotOrchestrator = timeSlotOrchestrator;
            _queueOrchestrator = queueOrchestrator;
            _timeSlotProcessService = timeSlotProcessService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeSlotBm>>> GetByQueue([FromQuery] int queueId)
        {
            var timeSlots = await _timeSlotOrchestrator.GetAllTimeSlotsForQueueAsync(queueId);
            return Ok(timeSlots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeSlotBm>> GetById(int id)
        {
            if (!await _timeSlotOrchestrator.CheckIfTimeSlotExistsAsync(id))
            {
                return new NotFoundResult();
            }

            return Ok(await _timeSlotOrchestrator.GetTimeSlotByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TimeSlotTemplate template)
        {
            return await _timeSlotProcessService.ProcessTimeSlotAdditionAsync(template);
        }

        [HttpPut("{id}/select")]
        public async Task<ActionResult> SelectSlot(int id, [FromQuery] string userId)
        {
            if (!await _timeSlotOrchestrator.CheckIfTimeSlotExistsAsync(id))
            {
                return new NotFoundResult();
            }

            return await _timeSlotProcessService.ProcessTimeSlotSelectionAsync(id, userId);
        }

        [HttpDelete()]
        public async Task<ActionResult> Delete([FromQuery] int queueId)
        {
            if (!await _queueOrchestrator.CheckIfQueueExistsAsync(queueId))
                return new NotFoundResult();

            await _timeSlotOrchestrator.ClearTimeSlotAsync(queueId);
            return new NoContentResult();
        }
    }
}