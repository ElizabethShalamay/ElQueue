using ElQueue.DAL.Repositories;
using ElQueue.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ElQueue.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IQueueRepository Queues { get; }
        IAccountRepository Accounts { get; }
        IUserRepository Users { get; }
        ITimeSlotRepository TimeSlots { get; }

        Task SaveAsync();
    }
}
