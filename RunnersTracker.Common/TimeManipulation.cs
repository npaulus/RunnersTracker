using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnersTracker.Common
{
    public static class TimeManipulation
    {
        public static DateTime SetTimeToEndOfDay(DateTime end)
        {
            DateTime result = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
            return result;

        }
    }
}
