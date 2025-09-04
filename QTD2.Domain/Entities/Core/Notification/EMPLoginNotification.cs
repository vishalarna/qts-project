using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPLoginNotification : Notification
    {
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public EMPLoginNotification() { }

        public EMPLoginNotification(DateTime dueDate, int employeeId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            EmployeeId = employeeId;
        }
    }
}
