using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTrackerDB;

namespace RunnersTracker.DataAccess
{
    public class LogEntriesDAC
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool AddNewActivity(LogEntry le)
        {
            var ctx = new RunnersTrackerContext();
            try
            {
                ctx.LogEntries.Add(le);
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                logger.Info(e.StackTrace);
                return false;
            }
            return true;
        }
    }
}
