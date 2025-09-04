using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CertificationSubRequirement
{
    public class CertificationSubRequirementCreateOptions
    {
        public int CertificationId { get; set; }

        public string[] ReqName { get; set; }

        public float[] ReqHour { get; set; }

        public int[] CertificationSubRequirementsIds { get; set; }
    }
}
