using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ElQueue.DAL.Infrastructure;
using ElQueue.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ElQueue.DAL.Repositories
{
    public class QueueRepository : RepositoryBase<Queue>, IQueueRepository
    {
        public QueueRepository(QueueContext context)
        {
            _context = context;
        }

        public async Task<Queue> GetAsync(int id)
        {
            var queues = await _context.Queues
                .Include(queue => queue.Account)
                .Include(queue => queue.TimeSlots)
                .ThenInclude(timeSlot => timeSlot.User)
                .ToListAsync();

            return queues.FirstOrDefault(queue => queue.Id == id);

        }

        public async Task<IEnumerable<Queue>> GetAsync(Expression<Func<Queue, bool>> predicate)
        {
            var queues = await _context.Queues
                .Include(queue => queue.Account)
                .Include(queue => queue.TimeSlots)
                .ThenInclude(timeSlot => timeSlot.User)
                .Where(predicate)
                .ToListAsync();

            return queues;
        }

        public async Task<IEnumerable<Queue>> GetAsync()
        {
            return await _context.Queues
                .Include(queue => queue.Account)
                .Include(queue => queue.TimeSlots)
                .ThenInclude(timeSlot => timeSlot.User)
                .ToListAsync();
        }

        public async Task CreateAsync(Queue entity)
        {
            await _context.Queues.AddAsync(entity);
        }

        public void Update(Queue entity)
        {
            _context.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var queueToDelete = await _context.Queues.FindAsync(id);
            _context.Queues.Remove(queueToDelete);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Queues.AsNoTracking().AnyAsync(queue => queue.Id == id);
        }

        public async Task<bool> FreeQueueNameExists(int id, string name)
        {
            return await _context.Queues.AsNoTracking().AnyAsync(queue => queue.Id != id && queue.Name.Equals(name));
        }
    }
}
