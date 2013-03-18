using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace RunnersTracker.WebUI.Models
{
    public class NewPasswordModel
    {
        [Required(ErrorMessage = "Please Enter Your Password.")]
        [StringLength(18)]
        [Display(Name = "Password", Description = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password.")]
        [StringLength(18)]
        [Display(Name = "Confirm Password", Description = "Confirm Password")]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}