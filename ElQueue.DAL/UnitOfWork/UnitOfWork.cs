using System.Threading.Tasks;
using ElQueue.DAL.Infrastructure;
using ElQueue.DAL.Repositories;
using ElQueue.DAL.Repositories.Interfaces;

namespace ElQueue.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QueueContext _context;

        private QueueRepository _queueRepository;
        private AccountRepository _accountRepository;
        private UserRepository _userRepository;
        private TimeSlotRepository _timeSlotRepository;

        public UnitOfWork(QueueContext context)
        {
            _context = context;
        }

        public IQueueRepository Queues => _queueRepository ?? new QueueRepository(_context);

        public IAccountRepository Accounts => _accountRepository ?? new AccountRepository(_context);

        public IUserRepository Users => _userRepository ?? new UserRepository(_context);

        public ITimeSlotRepository TimeSlots => _timeSlotRepository ?? new TimeSlotRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
