using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTracker.DataAccess;
using RunnersTrackerDB;
using RunnersTracker.Common;

namespace RunnersTracker.UnitTests
{
    public class UnitOfWorkTest : IUnitOfWork
    {
        private IGenericRepository<User> userRepository;
        private IGenericRepository<LogEntry> logEntryRepository;
        private IGenericRepository<ActivityTypes> activityTypesRepository;
        private IGenericRepository<Shoe> shoeRepository;

        public IGenericRepository<User> UserRepository
        {
            get
            {                
                return userRepository;
            }
        }

        public IGenericRepository<LogEntry> LogEntryRepository {
            get
            {
                return logEntryRepository;
            }
        }

        public IGenericRepository<ActivityTypes> ActivityTypesRepository
        {
            get
            {
                return activityTypesRepository;
            }
        }
        public IGenericRepository<Shoe> ShoeRepository
        {
            get
            {
                return shoeRepository;
            }
        }

        public UnitOfWorkTest(byte[] pass, byte[] salt)
        {
            IQueryable<User> repository = new List<User>()
            {
                new User { FirstName = "Test", LastName = "LastName", Email = "test@test.com", Salt = salt, Password = pass, AccountConfirmed = true},
                new User { FirstName = "Test", LastName = "LastName", Email = "test2@test.com", Salt = salt, Password = pass, AccountConfirmed = false}
            }.AsQueryable();
            userRepository = new GenericRepositoryTest<User>(repository);
        }

        public void Save()
        {

        }

    }
}
