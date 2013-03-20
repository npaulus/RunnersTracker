using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RunnersTracker.WebUI.Models;

namespace RunnersTracker.WebUI.Controllers
{
    public class LogController : Controller
    {
        //
        // GET: /Log/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddActivity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddActivity(LogEntryModel model)
        {
            return RedirectToAction("Index", "Log");
        }

    }
}
