using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RunnersTracker.Business.DTO;
using RunnersTracker.WebUI.Models;

namespace RunnersTracker.WebUI.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            ReportDTO report = new ReportDTO();

            return View(report);
        }

    }
}
