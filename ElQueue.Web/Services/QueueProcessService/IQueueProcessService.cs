using ElQueue.Orchestrator.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElQueue.Web.Services
{
    public interface IQueueProcessService
    {
        bool ShouldCreateQueue(int? id);

        Task<ActionResult> ProcessQueueAdditionAsync(NewQueueDto newQueueDto);
        Task<ActionResult> ProcessQueueUpdateAsync(NewQueueDto newQueueDto, int id);
    }
}
