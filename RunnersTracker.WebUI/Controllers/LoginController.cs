using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service;
using RunnersTracker.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RunnersTracker.Business.Service.Impl;

namespace RunnersTracker.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public log4net.ILog Logger { get { return _logger; } }


        private ILoginService loginService;
        public LoginController(ILoginService _loginService)
        {
            this.loginService = _loginService;
        }
        //
        // GET: /Login/
        [HttpGet]
        
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult LoginIndex(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = loginService.Login(model.Username, model.Password);
                if (user == null)
                {
                    TempData["LoginFailed"] = "Login Failed!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        user.Email,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        false,
                        user.Email);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Session["User"] = user;
                    Response.Cookies.Add(faCookie);
                    return RedirectToAction("Summary", "Summary");
                }                
            }
            TempData["LoginFailed"] = "Login Failed!";
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = loginService.Login(model.Username, model.Password);
                if (user == null)
                {
                    ViewBag.LoginFailed = "Login Failed!";
                    return View();
                }
                else
                {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        user.Email,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        false,
                        user.Email);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Session["User"] = user;
                    Response.Cookies.Add(faCookie);
                    return RedirectToAction("Summary", "Summary");
                }
                
            }
            ViewBag.LoginFailed = "Login Failed!";
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View("Reset");
        }

        [HttpPost]
        public ActionResult ResetPassword(string email)
        {
            if (loginService.ResetPassword(email))
            {
                TempData["ResetResult"] = true;
            }
            else
            {
                TempData["ResetResult"] = false;
            }
            
            return RedirectToAction("ResetPassword", "login");
        }

        [HttpGet]
        public ActionResult NewPassword(string code)
        {
            Session["code"] = code;
            Logger.Info("Code is: " +code);
            return View("NewPassword");
        }

        [HttpPost]
        public ActionResult NewPassword(NewPasswordModel npModel)
        {
            if (ModelState.IsValid)
            {
                Logger.Info("Code is: " + Session["code"]);
                string code = (string)Session["code"];
                if (loginService.NewPassword(npModel.Password, code))
                {
                    TempData["PasswordReset"] = true;
                    Session["code"] = null;
                    return RedirectToAction("login", "login");
                }
                else
                {
                    ViewBag.Expired = true;
                    return View("NewPassword");
                }
            }
            else
            {                
                return View("NewPassword");
            }
        }
    }
}
