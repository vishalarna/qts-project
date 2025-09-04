using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeActivityNotification
{
    public class EmployeeActivityNotificationOptions
    {
        public int EmployeeId { get; set; }

        public int ActivityNotificationId { get; set; }

        public int[] ActivityNotificationIds { get; set; }
    }
}
