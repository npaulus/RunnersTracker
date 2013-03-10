using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnersTracker.DataAccess.Entities
{
    public class LogEntry
    {
        public string ActivityName { get; set; }
        public string ActivityType { get; set; }
        public DateTime StartTime { get; set; }
        public string TimeZone { get; set; }
        public int DurationInSeconds { get; set; }
        public decimal Distance { get; set; }
        public int calories { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int LogId { get; set; }

        public virtual User User { get; set; }
        public virtual Shoes Shoes { get; set; }
    }
}
