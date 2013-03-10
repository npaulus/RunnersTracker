using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnersTracker.DataAccess.Entities
{
    public class User
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TimeZone { get; set; }
        public string DistanceType { get; set; }
        public string Password { get; set; }
        public bool AccountConfirmed { get; set; }
        public string ConfirmCode { get; set; }
        public string PassResetCode { get; set; }
        public DateTime PassResetExpire { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Shoes> Shoes { get; set; }
        public virtual ICollection<LogEntry> LogEntries { get; set; }
    }
}
