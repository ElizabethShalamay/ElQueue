using ElQueue.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElQueue.DAL.Repositories
{
    public interface IQueueRepository
    {
        Task<Queue> GetAsync(int id);

        Task<IEnumerable<Queue>> GetAsync(Expression<Func<Queue, bool>> predicate);

        Task<IEnumerable<Queue>> GetAsync();

        Task CreateAsync(Queue entity);

        void Update(Queue entity);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
