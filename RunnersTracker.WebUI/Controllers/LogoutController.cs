using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RunnersTracker.WebUI.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Index()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();            
            return RedirectToAction("Index", "Home");
        }

    }
}
