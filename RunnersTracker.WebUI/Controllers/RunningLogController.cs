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
            System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
            ViewBag.TimeZones = tz;
            ViewBag.SelectTz = user.TimeZone;
            return View();
        }

        [HttpPost]
        public ActionResult AddActivity(LogEntryModel model)
        {
            if (ModelState.IsValid)
            {
                
                UserDTO user = (UserDTO)Session["User"];
                LogEntryDTO logEntry = new LogEntryDTO();

                logEntry.UserUserId = user.UserId;
                logEntry.ActivityName = model.ActivityName;
                logEntry.ActivityType_Id = model.ActivityType;
                logEntry.Calories = model.Calories;
                logEntry.Description = model.Description;
                logEntry.Distance = model.Distance;
                logEntry.Duration = model.hours * 60 * 60 + model.minutes * 60 + model.seconds;
                logEntry.ShoeShoeId = model.ShoeId;
                logEntry.Tags = model.Tags;
                logEntry.TimeZone = model.TimeZone;

                if (runningLogService.AddActivity(logEntry))
                {
                    return RedirectToAction("Index", "RunningLog");
                }
                else
                {
                    ViewBag.AddActivityError = "true";
                    return View();
                }
            }
            else
            {
                UserDTO user = (UserDTO)Session["User"];
                ViewBag.ActivityTypes = runningLogService.ActivityTypes();
                ViewBag.UserShoes = runningLogService.GetUserShoes(user);
                System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
                ViewBag.TimeZones = tz;
                ViewBag.SelectTz = user.TimeZone;
                return View();
            }
        }

    }
}

