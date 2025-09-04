using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{ 
    public class PublicClassScheduleRequestNotification : Notification
    {
        public int PublicClassScheduleRequestNotification_PublicClassScheduleRequestId { get; set; }
        public virtual PublicClassScheduleRequest PublicClassScheduleRequest { get; set; }
        public PublicClassScheduleRequestNotification() { }

        public PublicClassScheduleRequestNotification(DateTime dueDate, int publicClassScheduleRequestId, int? toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            PublicClassScheduleRequestNotification_PublicClassScheduleRequestId = publicClassScheduleRequestId;
        }

        public PublicClassScheduleRequestNotification(DateTime dueDate, int publicClassScheduleRequestId,string othersEmailAddresses, int clientSettings_NotificationStepId) : base(dueDate, othersEmailAddresses, clientSettings_NotificationStepId)
        {
            PublicClassScheduleRequestNotification_PublicClassScheduleRequestId = publicClassScheduleRequestId;
        }
    }
}
