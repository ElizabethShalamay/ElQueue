using ElQueue.Orchestrator.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElQueue.Web.Services.TimeSlotProcessService
{
    public interface ITimeSlotProcessService
    {
        Task<ActionResult> ProcessTimeSlotAdditionAsync(TimeSlotTemplate template);
        Task<ActionResult> ProcessTimeSlotSelectionAsync(int id, string userId);
    }
}
