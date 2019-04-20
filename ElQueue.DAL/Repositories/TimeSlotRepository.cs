using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ElQueue.DAL.Infrastructure;
using ElQueue.DAL.Models;
using ElQueue.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElQueue.DAL.Repositories
{
    public class TimeSlotRepository : RepositoryBase<TimeSlot>, ITimeSlotRepository
    {
        public TimeSlotRepository(QueueContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(IEnumerable<TimeSlot> entities)
        {
            foreach(var entity in entities)
            {
                await _context.TimeSlots.AddAsync(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var timeSlotToDelete = await _context.TimeSlots.FindAsync(id);
            _context.TimeSlots.Remove(timeSlotToDelete);
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            var timeSlotsToDelete = await GetAsync(timeSlot => ids.Contains(timeSlot.Id));
            _context.TimeSlots.RemoveRange(timeSlotsToDelete);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.TimeSlots.AsNoTracking().AnyAsync(timeSlot => timeSlot.Id == id);
        }

        public async Task<TimeSlot> GetAsync(int id)
        {
            var timeSlots = await _context.TimeSlots
                .Include(timeSlot => timeSlot.Queue)
                .Include(timeSlot => timeSlot.User)
                .ToListAsync();

            return timeSlots.FirstOrDefault(timeSlot => timeSlot.Id == id);
        }

        public async Task<IEnumerable<TimeSlot>> GetAsync(Expression<Func<TimeSlot, bool>> predicate)
        {
            var timeSlots = await _context.TimeSlots
                 .Include(timeSlot => timeSlot.Queue)
                 .Include(timeSlot => timeSlot.User)
                 .Where(predicate)
                 .ToListAsync();

            return timeSlots;
        }

        public async Task<IEnumerable<TimeSlot>> GetAsync()
        {
            return await _context.TimeSlots
                .Include(timeSlot => timeSlot.Queue)
                .Include(timeSlot => timeSlot.User)
                .ToListAsync();
        }

        public void Update(TimeSlot entity)
        {
            _context.Update(entity);
        }
    }
}
