using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service;
using RunnersTracker.Business.DTO;
using RunnersTracker.Business.Service.Impl;
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
        private IRegisterService registerService;
        private static readonly ILog logger = LogManager.GetLogger(typeof(RegisterController));

        public RegisterController(IRegisterService _registerService)
        {
            this.registerService = _registerService;
        }

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
                user.Password = Encoding.UTF8.GetBytes(registerModel.Password);
                user.TimeZone = registerModel.TimeZone;
                
                if (registerService.createNewUser(user))
                {
                    logger.Info("Register worked");
                    return RedirectToAction("RegisterComplete");
                }
                else
                {
                    ViewBag.UserExists = false;
                    
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

        [HttpGet]
        public ActionResult Confirm(string code)
        {
            logger.Info(code);
            if (registerService.ConfirmUser(code))
            {
                logger.Info("register service returned true");
                TempData["Confirm"] = "Account Confirmed!";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.Confirm = false;
                return View("Register");
            }
        }

        [HttpGet]
        public ActionResult Resend()
        {
            return View("Resend");
        }

        [HttpPost]
        public ActionResult Resend(string email)
        {
            ViewBag.ResendResult = registerService.ResendConfirmationLink(email);
            
            return View("Resend");
        }
    }
}
