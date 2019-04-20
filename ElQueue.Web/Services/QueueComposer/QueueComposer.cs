using AutoMapper;
using ElQueue.Orchestrator.Dtos;

namespace ElQueue.Web.Services.QueueComposer
{
    public class QueueComposer : IQueueComposer
    {
        private readonly IMapper _mapper;

        public QueueComposer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public QueueToUpdateDto ComposeQueueForUpdate(NewQueueDto queueDto, int id)
        {
            var queueForUpdate = _mapper.Map<QueueToUpdateDto>(queueDto);
            queueForUpdate.Id = id;
            return queueForUpdate;
        }
    }
}
