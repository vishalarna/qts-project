using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Certifications.Models
{
    public class CertificationSubRequirement
    {
        public int CertificationSubRequirementId { get; set; }
        public string CertificationSubRequirementName { get; set; }
        public double RequiredHours { get; set; }
    }
}
