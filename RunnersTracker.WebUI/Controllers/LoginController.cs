using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service;
using RunnersTracker.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RunnersTracker.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public log4net.ILog Logger { get { return _logger; } }


        private LoginService loginService = new LoginService();
        //
        // GET: /Login/
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            Logger.Info("test logging");
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
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
                    return RedirectToAction("Summary", "Account");
                }                
            }
            TempData["LoginFailed"] = "Login Failed!";
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [AllowAnonymous]
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
                    return RedirectToAction("Summary", "Account");
                }
                
            }
            ViewBag.LoginFailed = "Login Failed!";
            return View();
        }



    }
}
