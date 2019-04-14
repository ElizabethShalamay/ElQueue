using ElQueue.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElQueue.DAL.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAsync(int id);

        Task<IEnumerable<Account>> GetAsync(Expression<Func<Account, bool>> predicate);

        Task<IEnumerable<Account>> GetAsync();

        Task CreateAsync(Account entity);

        Task DeleteAsync(int id);

        void UpdateAsync(Account entity);

        Task<bool> ExistsAsync(int id);
    }
}
