using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;


namespace QTD2.Domain.Entities.Core
{
    public class Certification_History  : Entity
    {
        public Certification_History (int certID, DateTime effectivedate, string notes)
        {
            CertId = certID;
            EffectiveDate = effectivedate;
            Notes = notes;
        }

        public Certification_History()
        {

        }

        public int CertId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Notes { get; set; }

        public virtual Certification Certification { get; set; }

    }
}
