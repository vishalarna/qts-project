using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class CertificationSubRequirement : Common.Entity
    {
        public int CertificationId { get; set; }

        public string ReqName { get; set; }

        public double ReqHour { get; set; }

        public virtual Certification Certification { get; set; }
        public virtual ICollection<ILACertificationSubRequirementLink> ILACertificationSubRequirementLinks { get; set; } = new List<ILACertificationSubRequirementLink>();

        public CertificationSubRequirement() { }

        public CertificationSubRequirement(int certificationId, string reqName, int reqHour)
        {
            CertificationId = certificationId;
            ReqName = reqName;
            ReqHour = reqHour;
        }
    }
}
