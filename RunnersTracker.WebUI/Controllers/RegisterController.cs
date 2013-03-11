using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service;
using RunnersTracker.Business.DTO; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RunnersTracker.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        RegisterService registerService = new RegisterService();

        //
        // GET: /Register/

        public ActionResult Register()
        {
            RegisterModel registerModel = new RegisterModel();

            return View(registerModel);
        }

        [HttpPost]
        public ActionResult doRegister(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                //convert form input to userDTO
                UserDTO user = new UserDTO();
                user.FirstName = registerModel.FirstName;
                user.LastName = registerModel.LastName;
                user.Email = registerModel.Email;
                user.Password = registerModel.Password;
                user.TimeZone = registerModel.TimeZone;
                
                if (registerService.createNewUser(user))
                {
                    return RedirectToAction("RegisterComplete");
                }
                else
                {
                    ViewBag.UserExists = "This user already exists.  Did you forget your password?";
                    
                    return View("Register");
                }
            }
            
            return View("Register");
        }

        [HttpGet]
        public ActionResult RegisterComplete()
        {
            return View("RegisterComplete");
        }

    }
}
