﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ElQueue.DAL.Infrastructure;
using ElQueue.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ElQueue.DAL.Repositories
{
    public class TimeSlotRepository : RepositoryBase<TimeSlot>, ITimeSlotRepository
    {
        public TimeSlotRepository(QueueContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TimeSlot entity)
        {
            await _context.TimeSlots.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var timeSlotToDelete = await _context.TimeSlots.FindAsync(id);
            _context.TimeSlots.Remove(timeSlotToDelete);
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
    }
}
