using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTrackerDB;

namespace RunnersTracker.DataAccess
{
    public interface IUnitOfWork  {        

        void Save();
        IGenericRepository<User> UserRepository {get;}
        IGenericRepository<LogEntry> LogEntryRepository { get; }
        IGenericRepository<ActivityTypes> ActivityTypesRepository { get; }
        IGenericRepository<Shoe> ShoeRepository { get; }

    }
}
