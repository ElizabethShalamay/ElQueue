using ElQueue.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElQueue.BLL.Services
{
    public interface IQueueService
    {
        Task<IEnumerable<Queue>> GetQueuesByAccount(int accountId);
    }
}
