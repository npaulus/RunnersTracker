using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RunnersTracker.WebUI.Models
{
    public class LogEntryModel
    {
        [StringLength(25,ErrorMessage = "Activity Name must be less than 25 characters")]
        public String ActivityName { get; set; }
        
        public String ActivityType { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime StartTime { get; set; }

        public int hours;

        public int minutes;

        public int seconds;
        
        public String TimeZone { get; set; }        

        public Decimal Distance { get; set; }

        public int Calories { get; set; }

        public String Description { get; set; }

        public String Tags { get; set; }

        public Int32 UserId { get; set; }

        public Int32 ShoeId { get; set; }
        
    }
}