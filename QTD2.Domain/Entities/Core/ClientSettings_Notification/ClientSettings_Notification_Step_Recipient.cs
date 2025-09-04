using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_Notification_Step_Recipient : Common.Entity
    {
        public ClientSettings_Notification_Step_Recipient() { }

        public ClientSettings_Notification_Step_Recipient(int employeeId)
        {
            EmployeeId = employeeId;
        }

        public int ClientSettingsNotificationStepId { get; set; }
        public int EmployeeId { get; set; }
    }
}
