using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElQueue.DAL.Models;
using ElQueue.DAL.UnitOfWork;

namespace ElQueue.BLL.Services
{
    public class QueueService : IQueueService
    {
        private readonly IUnitOfWork _storage;

        public QueueService(IUnitOfWork unitOfWork)
        {
            _storage = unitOfWork;
        }

        public async Task<IEnumerable<Queue>> GetQueuesByAccount(int accountId)
        {
            return await _storage.Queues.GetAsync(queue => queue.AccountId == accountId);
        }
    }
}
