using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QTD2.Infrastructure.Model.Certification
{
    public class CertificationCreateOptions
    {
        public int CertifyingBodyId { get; set; }

        public string CertAcronym { get; set; }
        public string Name { get; set; }
        public string CertDesc { get; set; }

        public bool? RenewalTimeFrame { get; set; }
        public int? RenewalInterval { get; set; }

        public bool? CreditHrsReq { get; set; }
        public float? CreditHrs { get; set; }

        public bool? CertSubReq { get; set; }

        public string[] CertSubReqName { get; set; }
        public float[] CertSubReqHours { get; set; }

        public bool? CertMemberNum { get; set; }
        public bool? CertifiedDate { get; set; }
        public bool? RenewalDate { get; set; }
        public bool? ExpirationDate { get; set; }
        public bool? AllowRolloverHours { get; set; }
        public float? AdditionalHours { get; set; }

        public string Notes { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int[] CertificationSubRequirementsIds { get; set; }
    }
}
