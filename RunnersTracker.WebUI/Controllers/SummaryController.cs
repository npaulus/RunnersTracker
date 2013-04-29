using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RunnersTracker.Business.Service.Interface;
using RunnersTracker.Business.DTO;
using RunnersTracker.WebUI.Models;
using RunnersTracker.Common;

namespace RunnersTracker.WebUI.Controllers
{
    public class SummaryController : Controller
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IReportService reportService;

        public SummaryController(IReportService _reportService)
        {
            reportService = _reportService;
        }

        public ActionResult Summary()
        {
            UserDTO user = (UserDTO)Session["User"];
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime begin = DateTime.Now.AddDays(-days).Date;
            DateTime end = TimeManipulation.SetTimeToEndOfDay(begin.AddDays(6).Date);
            
            ReportSummaryModel model = new ReportSummaryModel();
            ReportDTO weeklyReport = reportService.GetReportForTimePeriod(begin, end, user);
            model.Reports.Add("weekly", weeklyReport);

            DateTime monthBegin = new DateTime(begin.Year, begin.Month, 1, 0, 0, 0);
            logger.Info("Month begin: " + monthBegin.ToString());
            DateTime monthEnd = TimeManipulation.SetTimeToEndOfDay(DateTime.Today);
            ReportDTO monthlyReport = reportService.GetReportForTimePeriod(monthBegin, monthEnd, user);
            model.Reports.Add("monthly", monthlyReport);

            DateTime yearBegin = new DateTime(begin.Year, 1, 1, 0, 0, 0);
            logger.Info("Year Begin: " + yearBegin.ToString());
            DateTime yearEnd = TimeManipulation.SetTimeToEndOfDay(DateTime.Today);
            ReportDTO yearReport = reportService.GetReportForTimePeriod(yearBegin, yearEnd, user);
            model.Reports.Add("yearly", yearReport);

            return View(model);
        }

       

    }
}
