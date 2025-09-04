using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassScheduleEmployee_ILACertificationLink_PartialCredit : Common.Entity
    {
        public int ClassScheduleEmployeeId { get; set; }
        public int ILACertificationLinkId { get; set; }
        public double? PartialCreditHours { get; set; }

        public virtual ClassSchedule_Employee ClassSchedule_Employee { get; set; }
        public virtual ILACertificationLink ILACertificationLink { get; set; }
        public virtual List<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit> ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits { get; set; } = new List<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>();

        public ClassScheduleEmployee_ILACertificationLink_PartialCredit() { }

        public ClassScheduleEmployee_ILACertificationLink_PartialCredit(int classScheduleEmployeeId, int iLACertificationLinkId, double? partialCreditHours)
        {
            ClassScheduleEmployeeId = classScheduleEmployeeId;
            ILACertificationLinkId = iLACertificationLinkId;
            PartialCreditHours = partialCreditHours;
        }
    }
}
