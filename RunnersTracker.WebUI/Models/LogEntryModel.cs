using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using log4net;

namespace RunnersTracker.WebUI.Models
{
    public class LogEntryModel
    {
        

        [StringLength(25,ErrorMessage = "Activity Name must be less than 25 characters")]
        [Display(Name = "Activity Name ", Description = "")]
        public String ActivityName { get; set; }

        [Required(ErrorMessage = "Activity Type is required")]
        [Display(Name = "Activity Type ", Description = "")]
        public int ActivityType { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date)] 
        [StartDate(ErrorMessage = "Date must be today's date or earlier, but not less than 1/1/1930")]
        [Display(Name = "Start Date", Description = "")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Start Time", Description = "")]
        [RegularExpression(@"([1][0-2]|\d):([0-5]\d)\s(AM|PM)", ErrorMessage = "Please enter time in this format 05:00 PM")]
        public string StartTime { get; set; }

        [Display(Name = "Hours", Description = "")]
        public int hours {get; set;}

        [Required(ErrorMessage = "Please enter minutes")]
        [Display(Name = "Minutes", Description = "")]
        public int minutes { get; set; }

        [Required(ErrorMessage = "Please enter seconds")]
        [Display(Name = "Seconds", Description = "")]
        public int seconds { get; set; }

        [Display(Name = "Time Zone ", Description = "")]
        public String TimeZone { get; set; }

        [Required(ErrorMessage = "Please enter distance")]
        [Display(Name = "Distance ", Description = "")]
        public Decimal Distance { get; set; }

        [Display(Name = "Calories ", Description = "")]
        public int Calories { get; set; }

        [Display(Name = "Description ", Description = "")]
        public String Description { get; set; }

        [Display(Name = "Tags ", Description = "")]
        public String Tags { get; set; }

        public Int32 UserId { get; set; }

        [Display(Name = "Shoes ", Description = "")]
        public Int32 ShoeId { get; set; }
        
    }

    public sealed class StartDateAttribute : ValidationAttribute
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool IsValid(object value)
        {

            DateTime startDate = (DateTime) value;
            logger.Info("Date from form: " + startDate.ToShortDateString());
            if (startDate >= DateTime.Now)
            {
                return false;
            }
            else if (startDate < new DateTime(1930, 1, 1))
            {
                return false;
            }
            else
            {
                return true;
            }           
        }
    }
}