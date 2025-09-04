using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnClassSchedule_StudentEvaluations_StudentEvaluationReleased : Common.IDomainEvent, INotification
    {
        public ClassSchedule_Evaluation_Roster ClassSchedule_Evaluation_Roster { get; }
        public int ClassSchedule_Evaluation_RosterId { get; set; }

        public OnClassSchedule_StudentEvaluations_StudentEvaluationReleased(ClassSchedule_Evaluation_Roster classSchedule_Evaluation_Roster)
        {
            ClassSchedule_Evaluation_Roster = classSchedule_Evaluation_Roster;
        }
    }
}
