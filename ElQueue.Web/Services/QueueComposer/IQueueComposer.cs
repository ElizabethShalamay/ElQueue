using ElQueue.Orchestrator.Dtos;

namespace ElQueue.Web.Services.QueueComposer
{
    public interface IQueueComposer
    {
        QueueToUpdateDto ComposeQueueForUpdate(NewQueueDto queueDto, int id);
    }
}
