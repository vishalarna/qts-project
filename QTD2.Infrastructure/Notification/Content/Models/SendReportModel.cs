using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class SendReportModel
    {
        public string InternalReportTitle { get; set; }

        public SendReportModel(string internalReportTitle)
        {
            InternalReportTitle = internalReportTitle;
        }
    }
}
