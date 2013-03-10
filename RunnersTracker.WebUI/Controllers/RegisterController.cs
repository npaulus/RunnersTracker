using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service; 
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
                return RedirectToAction("Index"); 
            }
            return View();
        }

    }
}
