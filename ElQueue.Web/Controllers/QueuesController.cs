using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElQueue.BLL.Models;
using ElQueue.Orchestrator.QueueOrchestrator;
using Microsoft.AspNetCore.Mvc;

namespace ElQueue.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueOrchestrator _queueOrchestrator;
        private readonly IMapper _mapper;

        public QueuesController(IQueueOrchestrator queueOrchestrator, IMapper mapper)
        {
            _queueOrchestrator = queueOrchestrator;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueBm>>> Get()
        {
            var queues = await _queueOrchestrator.GetAllQueuesAsync();
            return Ok(queues);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueBm>> Get(int id)
        {
            if(! await _queueOrchestrator.CheckIfQueueExistsAsync(id))
            {
                return new NotFoundResult();
            }

            return Ok(await _queueOrchestrator.GetQueueByIdAsync(id));
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
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
