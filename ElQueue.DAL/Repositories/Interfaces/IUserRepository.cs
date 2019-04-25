using ElQueue.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElQueue.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string id);

        Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>> predicate);

        Task<IEnumerable<User>> GetAsync();

        Task CreateAsync(User entity);

        Task DeleteAsync(string id);

        void UpdateAsync(User entity);

        Task<bool> ExistsAsync(string id);
    }
}
