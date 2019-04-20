using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using ElQueue.Orchestrator.Dtos;
using ElQueue.Orchestrator.QueueOrchestrator;
using ElQueue.Web.Services.QueueComposer;
using Microsoft.AspNetCore.Mvc;

namespace ElQueue.Web.Services
{
    public class QueueProcessService : IQueueProcessService
    {
        private readonly IQueueOrchestrator _queueOrchestrator;
        private readonly IMapper _mapper;
        private readonly IQueueComposer _queueComposer;

        public QueueProcessService(IQueueOrchestrator queueOrchestrator, IQueueComposer queueComposer, IMapper mapper)
        {
            _queueOrchestrator = queueOrchestrator;
            _queueComposer = queueComposer;
            _mapper = mapper;
        }

        public bool ShouldCreateQueue(int? id)
        {
            return !id.HasValue;
        }

        public async Task<ActionResult> ProcessQueueAdditionAsync(NewQueueDto newQueueDto)
        {
            Guard.Argument(() => newQueueDto).NotNull();

            var gameBM = await _queueOrchestrator.CreateQueueAsync(newQueueDto);

            return new CreatedAtRouteResult(new { Id = gameBM.Id }, gameBM);
        }

        public async Task<ActionResult> ProcessQueueUpdateAsync(NewQueueDto newQueueDto, int id)
        {
            Guard.Argument(() => newQueueDto).NotNull();

            var queueDto = _queueComposer.ComposeQueueForUpdate(newQueueDto, id);
            await _queueOrchestrator.UpdateQueueAsync(queueDto);

            return new NoContentResult();
        }
    }
}
