using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RunnersTracker.Business.DTO;

namespace RunnersTracker.WebUI.Models
{
    public class ReportSummaryModel
    {
        public ReportSummaryModel()
        {
            Reports = new Dictionary<String, ReportDTO>();
        }

        //use dictionary to store weekly, monthly, and ytd totals
        public IDictionary<string, ReportDTO> Reports { get; set; }
    }
}