using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ILACertificationSubRequirementLink: Common.Entity
    {
        public int ILACertificationLinkId { get; set; }
        public int CertificationSubRequirementId { get; set; }
        public double ReqHour { get; set; }

        public virtual ILACertificationLink CertificationLink { get; set; }
        public virtual CertificationSubRequirement CertificationSubRequirement { get; set; }
        public virtual List<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit> ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits { get; set; } = new List<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>();

        public ILACertificationSubRequirementLink(int ilaCertificationLinkId, int certificationSubRequirementId, double reqHour)
        {
            ILACertificationLinkId = ilaCertificationLinkId;
            CertificationSubRequirementId = certificationSubRequirementId;
            ReqHour = reqHour;
        }
        public ILACertificationSubRequirementLink()
        {

        }

        public void UpdateReqHour(double reqHour)
        {
            ReqHour = reqHour;
        }
    }
}
