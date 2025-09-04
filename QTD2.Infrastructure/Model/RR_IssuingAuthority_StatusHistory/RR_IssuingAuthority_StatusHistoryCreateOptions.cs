using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RR_IssuingAuthority_StatusHistory
{
    public class RR_IssuingAuthority_StatusHistoryCreateOptions
    {
        public int RRIssuingAuthorityId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public DateTime ChangedOn { get; set; }

        public string ChangedBy { get; set; }

        public string ChangeNotes { get; set; }
    }
}
