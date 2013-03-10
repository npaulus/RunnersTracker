using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RunnersTracker.DataAccess.Entities;

namespace RunnersTracker.DataAccess.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void SaveUser(User user);
    }
}
