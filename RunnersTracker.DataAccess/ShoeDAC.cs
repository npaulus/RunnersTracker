using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using RunnersTrackerDB;

namespace RunnersTracker.DataAccess
{
    public class ShoeDAC
    {
        public ISet<Shoe> GetUserShoes(User user)
        {
            var ctx = new RunnersTrackerContext();
            ISet<Shoe> userShoes = new HashSet<Shoe>();
            var shoes = ctx.Shoes.Where(s => s.ShoeUserId == user.UserId);
            foreach (Shoe s in shoes)
            {
                userShoes.Add(s);
            }

            return userShoes;
        }
    }
}
