using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RunnersTracker.WebUI.Models
{
    public class ContactUsModel
    {
        [Required]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Name is required and must be 5 characters long")]
        [Display(Name = "Name", Description = "From Name")]
        public string FromName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email must be valid!")]        
        [Display(Name = "Email", Description = "Email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(700,MinimumLength = 20, ErrorMessage = "Must be at least 20 characters, but less than 700")]
        [Display(Name = "Comments", Description = "Enter feedback here.")]
        public string Comment { get; set; }

    }
}