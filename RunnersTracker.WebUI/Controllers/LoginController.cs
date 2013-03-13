using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RunnersTracker.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private LoginService loginService = new LoginService();
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
            if(loginService.Login(model.Username, model.Password) && ModelState.IsValid)
            {
                return RedirectToAction("Summary", "Account");
            }
            ViewBag.LoginFailed = "Login Failed!";
            return View("Index", "Home");
        }

    }
}
