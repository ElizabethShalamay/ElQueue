using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElQueue.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        T Get(int id);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAsync();

        IEnumerable<T> Get();

        Task CreateAsync(T entity);

        void Create(T entity);

        void Update(T entity);

        Task DeleteAsync(int id);

        void Delete(int id);

        Task<bool> ExistsAsync(int id);

    }
}