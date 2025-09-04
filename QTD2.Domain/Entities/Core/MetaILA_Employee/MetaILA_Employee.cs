using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class MetaILA_Employee : Common.Entity
    {
        public int EmployeeId { get; set; }
        public int MetaILAId { get; set; }
        public bool Enrolled { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual MetaILA MetaILA { get; set; }

        public virtual ICollection<MetaILA_Employee_MemberLinkFufillment> MetaILA_Employee_MemberLinkFufillments { get; set; }

        public MetaILA_Employee()
        {

        }

        public MetaILA_Employee(int employeeId, int metaILAId)
        {
            EmployeeId = employeeId;
            MetaILAId = metaILAId;
        }

        public void Fulfill(Meta_ILAMembers_Link member, ClassSchedule_Employee classSchedule_Employee)
        {
            AddEntityToNavigationProperty<MetaILA_Employee_MemberLinkFufillment>(new MetaILA_Employee_MemberLinkFufillment()
            {
                FufilledBy_ClassScheduleEmployeeId = classSchedule_Employee.Id,
                Meta_ILAMembers_LinkId = member.Id,
                MetaILA_EmployeeId = this.Id
            });
        }
    }
}
