using RunnersTrackerDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnersTracker.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private RunnersTrackerContext ctx = new RunnersTrackerContext();
        private GenericRepository<User> userRepository;
        private GenericRepository<LogEntry> logEntryRepository;
        private GenericRepository<ActivityTypes> activityTypesRepository;
        private GenericRepository<Shoe> shoeRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(ctx);
                }
                return userRepository;
            }
        }

        public GenericRepository<LogEntry> LogEntryRepository
        {
            get
            {
                if (this.logEntryRepository == null)
                {
                    this.logEntryRepository = new GenericRepository<LogEntry>(ctx);
                }
                return logEntryRepository;
            }
        }

        public GenericRepository<ActivityTypes> ActivityTypesRepository
        {
            get
            {
                if (this.activityTypesRepository == null)
                {
                    this.activityTypesRepository = new GenericRepository<ActivityTypes>(ctx);
                }
                return activityTypesRepository;
            }
        }

        public GenericRepository<Shoe> ShoeRepository
        {
            get
            {
                if (this.shoeRepository == null)
                {
                    this.shoeRepository = new GenericRepository<Shoe>(ctx);
                }
                return shoeRepository;
            }
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
