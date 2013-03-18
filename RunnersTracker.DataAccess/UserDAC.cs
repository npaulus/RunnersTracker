using RunnersTrackerDB;
using System.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace RunnersTracker.DataAccess
{
    public class UserDAC
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserDAC));

        public User RetrieveUserByEmail(string email)
        {
            var ctx = new RunnersTrackerContext();

            var findUser = ctx.Users.Where(u => u.Email.Equals(email));
            if (findUser.Count() == 1)
            {
                return findUser.First();
            }

            return null;
        }

        public User RetrieveUserByConfirmCode(string code)
        {
            if (code == null || code == "")
            {
                return null;
            }
            var ctx = new RunnersTrackerContext();
            var findUser = ctx.Users.Where(u => u.ConfirmCode.Equals(code));
            if (findUser.Count() == 1)
            {
                User user = findUser.First();
                return user;                
            }
            else
            {
                return null;
            }
        }

        public User RetrieveUserByResetPasswordCode(string code)
        {
            if (code == null || code == "")
            {
                return null;
            }
            var ctx = new RunnersTrackerContext();
            var findUser = ctx.Users.Where(u => u.PassResetCode.Equals(code));
            if (findUser.Count() == 1)
            {
                User user = findUser.First();
                return user;
            }
            else
            {
                return null;
            }
        }

        public User Save(User user)
        {
            var ctx = new RunnersTrackerContext();
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user;
        }

        public void Update(User user)
        {
            var ctx = new RunnersTrackerContext();
            User findUser = ctx.Users.Find(user.UserId);

            if (findUser != null)
            {
                ctx.Entry(findUser).CurrentValues.SetValues(user);
                ctx.SaveChanges();
            }            
        }

    }
}
