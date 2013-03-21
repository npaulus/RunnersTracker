using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using RunnersTrackerDB;

namespace RunnersTracker.DataAccess
{
    public class ActivityTypesDAC
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ActivityTypesDAC));

        public ISet<ActivityTypes> ActivityTypes()
        {
            var ctx = new RunnersTrackerContext();
            ISet<ActivityTypes> AllActivityTypes = new HashSet<ActivityTypes>();
            var ActivityTypes = ctx.ActivityTypes;
            foreach (ActivityTypes a in ActivityTypes)
            {
                AllActivityTypes.Add(a);
            }

            return AllActivityTypes;

        }

    }
}
