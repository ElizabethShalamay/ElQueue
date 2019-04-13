using ElQueue.DAL.Infrastructure;
using System;

namespace ElQueue.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IDisposable where T : class
    {
        protected QueueContext _context;        
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Already disposed");
            }

            if (disposing)
            {
                _context.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void DisposeFromUnitOfWork()
        {
            Dispose(false);
        }
    }

}
