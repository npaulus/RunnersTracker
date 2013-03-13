using RunnersTrackerDB;
using System.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnersTracker.DataAccess
{
    public class UserDAC
    {
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

        public User Save(User user)
        {
            var ctx = new RunnersTrackerContext();
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user;
        }
    }
}
