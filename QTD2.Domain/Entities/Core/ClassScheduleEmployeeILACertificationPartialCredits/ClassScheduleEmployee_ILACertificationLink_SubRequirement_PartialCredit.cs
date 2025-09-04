using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit : Common.Entity
    {
        public int ClassScheduleEmployee_ILACertificationLink_PartialCreditId { get; set; }
        public int ILACertificationSubRequirementLinkId { get; set; }
        public double? PartialCreditHours { get; set; }
        public ClassScheduleEmployee_ILACertificationLink_PartialCredit ClassScheduleEmployee_ILACertificationLink_PartialCredit { get; set; }
        public ILACertificationSubRequirementLink ILACertificationSubRequirementLink { get; set; }
        public ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit() { }
        public ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit(int classScheduleEmployee_ILACertificationLink_PartialCreditId, int iLACertificationSubRequirementLinkId, double? partialCreditHours)
        {
            ClassScheduleEmployee_ILACertificationLink_PartialCreditId = classScheduleEmployee_ILACertificationLink_PartialCreditId;
            ILACertificationSubRequirementLinkId = iLACertificationSubRequirementLinkId;
            PartialCreditHours = partialCreditHours;
        }
    }
}
