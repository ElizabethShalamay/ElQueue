using ElQueue.DAL.Infrastructure;
using ElQueue.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElQueue.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(QueueContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            _context.Users.Remove(userToDelete);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users.AsNoTracking().AnyAsync(user => user.Id == id);
        }

        public async Task<User> GetAsync(int id)
        {
            var users = await _context.Users
                .Include(user => user.TimeSlots)
                .ToListAsync();

            return users.FirstOrDefault(user => user.Id == id);
        }

        public async Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>> predicate)
        {
            var users = await _context.Users
                 .Include(user => user.TimeSlots)
                 .Where(predicate)
                 .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _context.Users
                .Include(user => user.TimeSlots)
                .ToListAsync();
        }

        public void UpdateAsync(User entity)
        {
            _context.Update(entity);
        }
    }
}
