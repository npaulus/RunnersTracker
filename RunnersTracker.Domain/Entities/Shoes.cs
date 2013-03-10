using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnersTracker.DataAccess.Entities
{
    public class Shoes
    {
        public string ShoeBrand { get; set; }
        public string ShoeModel { get; set; }
        public decimal ShoeDistance { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Active { get; set; }
        public int ShoeId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<LogEntry> LogEntry { get; set; }
    }
}
