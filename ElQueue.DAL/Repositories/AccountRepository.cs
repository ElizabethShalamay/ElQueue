using ElQueue.DAL.Infrastructure;
using ElQueue.DAL.Models;
using ElQueue.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElQueue.DAL.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(QueueContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Account entity)
        {
            await _context.Accounts.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var accountToDelete = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(accountToDelete);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Accounts.AsNoTracking().AnyAsync(account => account.Id == id);
        }

        public async Task<Account> GetAsync(int id)
        {
            var accounts = await _context.Accounts
                .Include(account => account.Queues)
                .ToListAsync();

            return accounts.FirstOrDefault(account => account.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAsync(Expression<Func<Account, bool>> predicate)
        {
            var accounts = await _context.Accounts
                 .Include(account => account.Queues)
                 .Where(predicate)
                 .ToListAsync();

            return accounts;
        }

        public async Task<IEnumerable<Account>> GetAsync()
        {
            return await _context.Accounts
                .Include(account => account.Queues)
                .ToListAsync();
        }

        public void UpdateAsync(Account entity)
        {
            _context.Update(entity);
        }
    }
}
