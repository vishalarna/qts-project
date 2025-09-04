using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class MetaIla_Admin_SelfRegistrationRequiredNotification : Notification
    {
        public int NextMeta_ILAMembers_LinkId { get; set; }
        public int? MetaILA_Employee_MemberLinkFufillmentId { get; set; }
        public int EmployeeId { get; set; }

        public virtual MetaILA_Employee_MemberLinkFufillment MetaILA_Employee_MemberLinkFufillment { get; set; }
        public virtual Meta_ILAMembers_Link NextMeta_ILAMembers_Link { get; set; }
        public virtual Employee Employee { get; set; }

        public MetaIla_Admin_SelfRegistrationRequiredNotification() { }

        public MetaIla_Admin_SelfRegistrationRequiredNotification(DateTime dueDate, int? metaILA_Employee_MemberLinkFufillmentId, int nextMeta_ILAMembers_LinkId, int employeeId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            MetaILA_Employee_MemberLinkFufillmentId = metaILA_Employee_MemberLinkFufillmentId;
            NextMeta_ILAMembers_LinkId = nextMeta_ILAMembers_LinkId;
            EmployeeId = employeeId;
        }
    }
}
