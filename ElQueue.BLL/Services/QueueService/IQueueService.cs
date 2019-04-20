using ElQueue.BLL.Models;
using ElQueue.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElQueue.BLL.Services.QueueService
{
    public interface IQueueService
    {
        Task<IEnumerable<Queue>> GetQueuesByAccount(int accountId);

        Task<QueueBm> CreateQueueAsync(QueueBm queueBm);

        Task UpdateQueueAsync(QueueBm queueBm);
    }
}
