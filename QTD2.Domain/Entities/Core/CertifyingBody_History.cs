using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class CertifyingBody_History : Entity
    {
        public CertifyingBody_History (int certifyingBodyId, DateTime effectivedate, string notes)
        {
            CertifyingBodyId = certifyingBodyId;
            EffectiveDate = effectivedate;
            Notes = notes;
        }

        public CertifyingBody_History()
        {

        }

        public int CertifyingBodyId { get; set; }
        public DateTime? EffectiveDate { get; set; }

        public string Notes { get; set; }

        public virtual CertifyingBody CertifyingBody { get; set; }
    }
}
