using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTracker.Business.DTO;

namespace RunnersTracker.Business.Service.Interface
{
    public interface IReportService
    {
        ReportDTO GetReportForTimePeriod(DateTime begin, DateTime end, UserDTO user);
    }
}
