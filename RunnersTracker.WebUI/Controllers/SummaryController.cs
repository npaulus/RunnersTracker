using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RunnersTracker.WebUI.Controllers
{
    public class SummaryController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View("Summary");
        }

        public ActionResult Summary()
        {
            return View();
        }

    }
}
