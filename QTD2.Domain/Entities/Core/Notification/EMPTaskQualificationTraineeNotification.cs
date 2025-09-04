using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPTaskQualificationTraineeNotification : Notification
    {
        public int TaskQualificationId { get; set; }

        public virtual TaskQualification TaskQualification { get; set; }

        public EMPTaskQualificationTraineeNotification() { }

        public EMPTaskQualificationTraineeNotification(DateTime dueDate, int taskQualificationId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            TaskQualificationId = taskQualificationId;
        }
    }
}
