using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service;
using RunnersTracker.Business.DTO; 
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace RunnersTracker.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {        
        RegisterService registerService = new RegisterService();
        private static readonly ILog logger = LogManager.GetLogger(typeof(RegisterController));
        //
        // GET: /Register/

        public ActionResult Register()
        {
            RegisterModel registerModel = new RegisterModel();
            logger.Info("Inside register action result");
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
                user.Password = Encoding.UTF8.GetBytes(registerModel.Password);
                user.TimeZone = registerModel.TimeZone;
                
                if (registerService.createNewUser(user))
                {
                    logger.Info("Register worked");
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
