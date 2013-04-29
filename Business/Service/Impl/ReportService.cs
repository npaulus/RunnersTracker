using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTracker.Business.Service.Interface;
using RunnersTracker.Business.DTO;
using RunnersTracker.DataAccess;
using RunnersTrackerDB;

namespace RunnersTracker.Business.Service.Impl
{
    public class ReportService : IReportService
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IUnitOfWork unitOfWork;

        public ReportService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public ReportDTO GetReportForTimePeriod(DateTime begin, DateTime end, UserDTO user)
        {
            logger.Info("Inside get report for time period");
            ReportDTO reportDTO = new ReportDTO();
            logger.Info("Begin time: " + begin.ToString());
            logger.Info("End time: " + end.ToString());
            IList<LogEntry> logEntries = unitOfWork.LogEntryRepository.Get(l => l.StartTime > begin && l.StartTime < end && l.UserId == user.UserId).ToList();
            logger.Info("Count in list: " + logEntries.Count());
            if (logEntries.Count() != 0)
            {
                logger.Info("Loading results");
                reportDTO.ActivityCount = logEntries.Count();
                reportDTO.Calories = (int)logEntries.Sum(l => l.Calories);
                reportDTO.Duration = logEntries.Sum(l => l.Duration);
                reportDTO.Distance = logEntries.Sum(l => l.Distance);
                logger.Info("Testing activity count: " + reportDTO.ActivityCount);
            }
            else
            {
                reportDTO.ActivityCount = 0;
                reportDTO.Calories = 0;
                reportDTO.Duration = 0;
                reportDTO.Distance = 0;
                logger.Info("no results");
            }
            return reportDTO;
        }
    }
}
