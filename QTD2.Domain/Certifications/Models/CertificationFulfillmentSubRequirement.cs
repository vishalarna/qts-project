using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Certifications.Models
{
    public class CertificationFulfillmentSubRequirement
    {
        public int CertificationSubRequirementId { get; set; }
        public string CertificationSubRequirementName { get; set; }
        public double Hours { get; set; }
        public double AwardedHours { get; set; }
        public double PendingHours { get; set; }
    }
}
