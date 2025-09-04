using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPTaskQualitificationEvaluatorNotification : Notification
    {
        public int TaskQualification_Evaluator_LinkId { get; set; }

        public virtual TaskQualification_Evaluator_Link TaskQualification_Evaluator_Link { get; set; }

        public EMPTaskQualitificationEvaluatorNotification() { }

        public EMPTaskQualitificationEvaluatorNotification(DateTime dueDate, int taskQualification_Evaluator_LinkId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            TaskQualification_Evaluator_LinkId = taskQualification_Evaluator_LinkId;
        }
    }
}
