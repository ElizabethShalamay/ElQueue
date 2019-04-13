using ElQueue.BLL.Models;
using ElQueue.Orchestrator.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElQueue.Orchestrator.QueueOrchestrator
{
    public interface IQueueOrchestrator
    {
        Task<QueueBm> GetQueueByIdAsync(int id);

        Task<IEnumerable<QueueBm>> GetAllQueuesAsync();

        Task<bool> CheckIfQueueExistsAsync(int id);

        Task<QueueBm> CreateQueueAsync(NewQueueDto gameDto);

        Task UpdateQueueAsync(QueueToUpdateDto gameDto);

        Task DeleteQueueAsync(int id);

        Task<IEnumerable<QueueBm>> GetQueuesByAccountIdAsync(int id);
    }
}
