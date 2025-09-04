using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class RR_IssuingAuthority_StatusHistory : Entity
    {
        public RR_IssuingAuthority_StatusHistory(
            int rRIssuingAuthorityId,
            bool oldStatus,
            bool newStatus,
            DateTime changeEffectiveDate,
            string changeNotes)
        {
            RRIssuingAuthorityId = rRIssuingAuthorityId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }

        public RR_IssuingAuthority_StatusHistory()
        {
        }

        public int RRIssuingAuthorityId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual RR_IssuingAuthority RR_IssuingAuthority { get; set; }
    }
}
