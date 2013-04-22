using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RunnersTracker.WebUI.Models;
using RunnersTracker.Business.Service.Impl;
using RunnersTracker.Business.Service.Interface;
using RunnersTracker.Business.DTO;
using log4net;

namespace RunnersTracker.WebUI.Controllers
{
    public class RunningLogController : Controller
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IRunningLogService runningLogService;
        public int PageSize = 5;

        public RunningLogController(IRunningLogService rls)
        {
            this.runningLogService = rls;
        }

        //
        // GET: /Log/

        public ActionResult Index(int page = 1)
        {
            UserDTO user = (UserDTO)Session["User"];
            LogEntryViewModel LogModel = new LogEntryViewModel();
            //setup paging variables
            LogModel.CurrentPage = page;
            LogModel.ItemsPerPage = PageSize;
            LogModel.TotalItems = runningLogService.GetCountOfUserLogEntries(user);
              
            //get activity types and their names
            LogModel.ActivityNames = new Dictionary<int, string>();
            IList<ActivityTypesDTO> activityTypes = runningLogService.ActivityTypes();
            foreach(ActivityTypesDTO activity in activityTypes)
            {
                LogModel.ActivityNames[activity.Id] = activity.ActivityType_Name;
            }            

            LogModel.UserLogEntries = runningLogService.GetUserRunningLogEntries(user, page);
            return View(LogModel);
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

                logEntry.UserId = user.UserId;
                logEntry.ActivityName = model.ActivityName;
                logEntry.ActivityTypesId = model.ActivityType;
                
                logEntry.Calories = model.Calories;
                logEntry.Description = model.Description;
                logEntry.Distance = model.Distance;
                int hours = 0;
                if (model.hours.HasValue)
                {
                    hours = (int) model.hours;
                }

                logEntry.Duration = hours * 60 * 60 + model.minutes * 60 + model.seconds;
                logEntry.ShoeId = model.ShoeId;
                logEntry.Tags = model.Tags;
                logEntry.TimeZone = model.TimeZone;
                logger.Info("Time after timespan: " + model.StartDate.Date.ToShortDateString() + " " + model.StartTime);
                DateTime combinedStartTime = DateTime.Parse(model.StartDate.Date.ToShortDateString() + " " + model.StartTime);
                logEntry.StartTime = combinedStartTime;
                logger.Info("Activity type id: " + logEntry.ActivityTypesId);
                logger.Info("Acitivty from model: " + model.ActivityType);

                if (runningLogService.AddActivity(logEntry, user))
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
                foreach (var item in ModelState)
                {
                    logger.Info("error key: " + item.Key);
                    var errors = item.Value.Errors;
                    foreach (var error in errors)
                    {
                        logger.Info("Error Values: " + error.ErrorMessage);
                    }
                    
                }
                UserDTO user = (UserDTO)Session["User"];
                ViewBag.ActivityTypes = runningLogService.ActivityTypes();
                ViewBag.UserShoes = runningLogService.GetUserShoes(user);
                System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
                ViewBag.TimeZones = tz;
                ViewBag.SelectTz = user.TimeZone;
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditActivity(int logId)
        {
            UserDTO user = (UserDTO) Session["User"];
            LogEntryModel model = new LogEntryModel();
            LogEntryDTO userActivity = runningLogService.GetActivity(logId, user);
            model.ActivityName = userActivity.ActivityName;
            model.ActivityType = userActivity.ActivityTypesId;
            model.StartDate = userActivity.StartTime;
            model.StartTime = userActivity.StartTime.ToShortTimeString();
            int hours, minutes, seconds;
            hours = userActivity.Duration / 3600;
            minutes = (userActivity.Duration - (3600 * hours)) / 60;
            seconds = (userActivity.Duration - (3600 * hours)) % 60;
            model.hours = hours;
            model.minutes = minutes;
            model.seconds = seconds;
            model.Distance = userActivity.Distance;
            model.Calories = userActivity.Calories.Value;
            model.Tags = userActivity.Tags;
            model.Description = userActivity.Description;
            model.ShoeId = userActivity.ShoeId;
            model.LogId = userActivity.LogId;

            ViewBag.ActivityTypes = runningLogService.ActivityTypes();
            ViewBag.UserShoes = runningLogService.GetUserShoes(user);
            System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
            ViewBag.TimeZones = tz;
            ViewBag.SelectTz = user.TimeZone;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditActivity(LogEntryModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = (UserDTO)Session["User"];
                LogEntryDTO logEntry = new LogEntryDTO();
                logEntry.LogId = model.LogId;
                logEntry.UserId = user.UserId;
                logEntry.ActivityName = model.ActivityName;
                logEntry.ActivityTypesId = model.ActivityType;

                logEntry.Calories = model.Calories;
                logEntry.Description = model.Description;
                logEntry.Distance = model.Distance;
                int hours = 0;
                if (model.hours.HasValue)
                {
                    hours = (int)model.hours;
                }

                logEntry.Duration = hours * 60 * 60 + model.minutes * 60 + model.seconds;
                logEntry.ShoeId = model.ShoeId;
                logEntry.Tags = model.Tags;
                logEntry.TimeZone = model.TimeZone;
                logger.Info("Time after timespan: " + model.StartDate.Date.ToShortDateString() + " " + model.StartTime);
                DateTime combinedStartTime = DateTime.Parse(model.StartDate.Date.ToShortDateString() + " " + model.StartTime);
                logEntry.StartTime = combinedStartTime;
                logger.Info("Activity type id: " + logEntry.ActivityTypesId);
                logger.Info("Acitivty from model: " + model.ActivityType);
                runningLogService.UpdateActivity(logEntry, user);
                return RedirectToAction("Index", "RunningLog");
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

        [HttpGet] 
        public ActionResult DeleteActivity(int logId)
        {
            UserDTO user = (UserDTO)Session["User"];
            runningLogService.DeleteEntry(logId, user);
            return RedirectToAction("Index", "RunningLog");
        }

    }
}

