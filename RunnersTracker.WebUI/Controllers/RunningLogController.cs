using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service;
using RunnersTracker.Business.DTO;
using log4net;

namespace RunnersTracker.WebUI.Controllers
{
    public class RunningLogController : Controller
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        RunningLogService runningLogService = new RunningLogService();
        //
        // GET: /Log/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddActivity()
        {
            UserDTO user = (UserDTO)Session["User"];
            ViewBag.ActivityTypes = runningLogService.ActivityTypes();
            ViewBag.UserShoes = runningLogService.GetUserShoes(user);
            
            return View();
        }

        [HttpPost]
        public ActionResult AddActivity(LogEntryModel model)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index", "RunningLog");
            }
            else
            {
                UserDTO user = (UserDTO)Session["User"];
                ViewBag.ActivityTypes = runningLogService.ActivityTypes();
                ViewBag.UserShoes = runningLogService.GetUserShoes(user);
                return View();
            }
        }

    }
}

