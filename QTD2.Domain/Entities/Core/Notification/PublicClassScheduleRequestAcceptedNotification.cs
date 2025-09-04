using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class PublicClassScheduleRequestAcceptedNotification: Notification
    {
        public int PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId { get; set; }
        public virtual PublicClassScheduleRequest PublicClassScheduleRequest { get; set; }
        public PublicClassScheduleRequestAcceptedNotification() { }

        public PublicClassScheduleRequestAcceptedNotification(DateTime dueDate, int publicClassScheduleRequestId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId = publicClassScheduleRequestId;
        }
    }
}
