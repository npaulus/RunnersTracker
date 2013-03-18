using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RunnersTracker.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter Email Address.")]
        [Display(Name = "Email", Description = "Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Description = "Password")]
        public string Password { get; set; }

    }
}