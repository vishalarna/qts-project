using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class MetaILA_Employee_MemberLinkFufillment : Common.Entity
    {
        public int MetaILA_EmployeeId { get; set; }
        public int Meta_ILAMembers_LinkId { get; set; }
        public int? FufilledBy_ClassScheduleEmployeeId { get; set; }

        public virtual Meta_ILAMembers_Link Meta_ILAMembers_Link { get; set; }
        public virtual MetaILA_Employee MetaILA_Employee { get; set; }
        public virtual ClassSchedule_Employee FufilledBy_ClassScheduleEmployee { get; set; }
    }
}
