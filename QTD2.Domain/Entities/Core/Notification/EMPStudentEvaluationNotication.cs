using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPStudentEvaluationNotication : Notification
    {
        public int ClassSchedule_Evaluation_RosterId { get; set; }

        public virtual ClassSchedule_Evaluation_Roster ClassSchedule_Evaluation_Roster { get; set; }

        public EMPStudentEvaluationNotication() { }

        public EMPStudentEvaluationNotication(DateTime dueDate, int classSchedule_Evaluation_RosterId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            ClassSchedule_Evaluation_RosterId = classSchedule_Evaluation_RosterId;
        }
    }
}
