using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnersTracker.Business.DTO
{
    public class ReportDTO
    {
        public int ActivityCount { get; set; }
        public decimal Distance { get; set; }
        public int Duration { get; set; }
        public int Calories { get; set; }

    }
}
