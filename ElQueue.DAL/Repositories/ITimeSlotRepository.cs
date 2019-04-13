using ElQueue.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElQueue.DAL.Repositories
{
    public interface ITimeSlotRepository
    {
        Task<TimeSlot> GetAsync(int id);

        Task<IEnumerable<TimeSlot>> GetAsync(Expression<Func<TimeSlot, bool>> predicate);

        Task<IEnumerable<TimeSlot>> GetAsync();

        Task CreateAsync(TimeSlot entity);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
