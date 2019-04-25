using System.Collections.Generic;
using System.Threading.Tasks;
using ElQueue.BLL.Models;
using ElQueue.Orchestrator.Dtos;
using ElQueue.Orchestrator.QueueOrchestrator;
using ElQueue.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElQueue.Web.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueOrchestrator _queueOrchestrator;
        private readonly IQueueProcessService _queueProcessService;

        public QueuesController(IQueueOrchestrator queueOrchestrator, IQueueProcessService queueProcessService)
        {
            _queueOrchestrator = queueOrchestrator;
            _queueProcessService = queueProcessService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueBm>>> Get()
        {
            var queues = await _queueOrchestrator.GetAllQueuesAsync();
            return Ok(queues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QueueBm>> Get(int id)
        {
            if(! await _queueOrchestrator.CheckIfQueueExistsAsync(id))
            {
                return new NotFoundResult();
            }

            return Ok(await _queueOrchestrator.GetQueueByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewQueueDto queueDto)
        {
            return await _queueProcessService.ProcessQueueAdditionAsync(queueDto);
        }

        [HttpPut()]
        public async Task<ActionResult> Put(int? id, [FromBody] NewQueueDto newQueueDto)
        {
            return _queueProcessService.ShouldCreateQueue(id)
                ? await _queueProcessService.ProcessQueueAdditionAsync(newQueueDto)
                : await _queueProcessService.ProcessQueueUpdateAsync(newQueueDto, (int)id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _queueOrchestrator.CheckIfQueueExistsAsync(id))
                return new NotFoundResult();

            await _queueOrchestrator.DeleteQueueAsync(id);
            return new NoContentResult();
        }
    }
}
