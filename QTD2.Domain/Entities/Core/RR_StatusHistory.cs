using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class RR_StatusHistory : Entity
    {
        public RR_StatusHistory(int regulatoryRequirementId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            RegulatoryRequirementId = regulatoryRequirementId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            this.ChangeNotes = changeNotes;
        }

        public RR_StatusHistory()
        {
        }

        public int RegulatoryRequirementId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual RegulatoryRequirement RegulatoryRequirement { get; set; }
    }
}
