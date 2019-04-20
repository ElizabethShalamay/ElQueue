using ElQueue.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElQueue.DAL.Repositories.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<TimeSlot> GetAsync(int id);

        Task<IEnumerable<TimeSlot>> GetAsync(Expression<Func<TimeSlot, bool>> predicate);

        Task<IEnumerable<TimeSlot>> GetAsync();

        Task CreateAsync(IEnumerable<TimeSlot> entity);

        Task DeleteAsync(int id);

        Task DeleteAsync(IEnumerable<int> id);

        Task<bool> ExistsAsync(int id);

        void Update(TimeSlot timeSlot);
    }
}
