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
            logger.Info("Total Items: " + LogModel.TotalItems);
            logger.Info("Total Pages: " + LogModel.TotalPages);    
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

    }
}

