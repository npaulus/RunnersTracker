using RunnersTracker.DataAccess.Abstract;
using RunnersTracker.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnersTracker.DataAccess.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private RunnersTrackerDbContext context = new RunnersTrackerDbContext();

        public IQueryable<User> Users
        {
            get { return context.Users; }
        }

        public void SaveUser(User user)
        {
            if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User EditUser = context.Users.Find(user.UserId);
                EditUser.FirstName = user.FirstName;
                EditUser.LastName = user.LastName;
                EditUser.Email = user.Email;
                EditUser.DistanceType = user.DistanceType;
                EditUser.TimeZone = user.TimeZone;
                EditUser.Password = user.Password;
                EditUser.AccountConfirmed = user.AccountConfirmed;
                EditUser.PassResetCode = user.PassResetCode;
                EditUser.ConfirmCode = user.ConfirmCode;
                EditUser.PassResetCode = user.PassResetCode;
                EditUser.PassResetExpire = user.PassResetExpire;
            }

        }

    }
}
