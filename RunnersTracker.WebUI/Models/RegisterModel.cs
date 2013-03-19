using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RunnersTracker.WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage="First Name is Required!")]
        [Display(Name = "First Name", Description="")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required!")]
        [Display(Name = "Last Name", Description = "")]
        public string LastName { get; set; }

        [Required(ErrorMessage="Email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage="Email must be valid!")]
        [Display(Name = "Email", Description = "")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Confirm Email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage="Email must be valid!")]
        [CompareAttribute("Email", ErrorMessage="Emails must match!")]
        [Display(Name = "Confirm Email", Description = "")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage="Password is required and must be less than 18 characters!")]
        [StringLength(18)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Description = "")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required and must be less than 18 characters!")]
        [StringLength(18)]
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match")]
        [Display(Name = "Confirm Password", Description = "")]        
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage="Time zone is required!")]
        [Display(Name = "Time Zone", Description = "")]
        public string TimeZone { get; set; }

    }
}
