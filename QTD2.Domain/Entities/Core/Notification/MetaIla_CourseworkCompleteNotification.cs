using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class MetaIla_CourseworkCompleteNotification : Notification
    {
        public int? ClassScheduleRosterId { get; set; }
        public int? ClassSchedule_Evaluation_RosterId { get; set; }
        public int MetaILA_Employee_MemberLinkFufillmentId { get; set; }
        public int EmployeeId { get; set; }

        public virtual ClassSchedule_Roster ClassSchedule_Roster { get; set; }
        public virtual ClassSchedule_Evaluation_Roster ClassSchedule_Evaluation_Roster { get; set; }
        public virtual Meta_ILAMembers_Link NextMeta_ILAMembers_Link { get; set; }
        public virtual Employee Employee { get; set; }

        public MetaIla_CourseworkCompleteNotification() { }

        public MetaIla_CourseworkCompleteNotification(DateTime dueDate, int metaILA_Employee_MemberLinkFufillmentId, int classSchedule_RosterId, int classSchedule_Evaluation_RosterId, int employeeId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            MetaILA_Employee_MemberLinkFufillmentId = metaILA_Employee_MemberLinkFufillmentId;
            ClassScheduleRosterId = classSchedule_RosterId;
            ClassSchedule_Evaluation_RosterId = classSchedule_Evaluation_RosterId;
            EmployeeId = employeeId;
        }
    }
}
