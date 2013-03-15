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

        public bool getUser(string email)
        {
            var ctx = new RunnersTrackerContext();

            var userQuery = ctx.Users.Where(u => u.Email.Equals(email));
            
            foreach (User u in userQuery)
            {
                if (u.Email.Equals(email))
                {
                    return true;
                }
            }
            
            return false;
        }

        public User RetrieveUser(string email)
        {
            var ctx = new RunnersTrackerContext();

            var findUser = ctx.Users.Where(u => u.Email.Equals(email));
            if (findUser.Count() == 1)
            {
                return findUser.First();
            }

            return null;
        }

        public bool ConfirmUserAccount(string code)
        {
            if (code == null || code == "")
            {
                return false;
            }
            var ctx = new RunnersTrackerContext();
            var findUser = ctx.Users.Where(u => u.ConfirmCode.Equals(code));
            if (findUser.Count() == 1)
            {
                User user = findUser.First();
                user.AccountConfirmed = true;
                user.ConfirmCode = "";
                logger.Info("Updated User: " + user.AccountConfirmed);
                logger.Info("Changes Saved: " + ctx.SaveChanges());
                return true;
            }
            else
            {
                return false;
            }
        }

        public User Save(User user)
        {
            var ctx = new RunnersTrackerContext();
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user;
        }
    }
}
