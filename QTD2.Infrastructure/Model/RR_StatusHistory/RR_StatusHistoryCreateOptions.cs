using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RR_StatusHistory
{
    public class RR_StatusHistoryCreateOptions
    {
        public RR_StatusHistoryCreateOptions(int regulatoryRequirementId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            RegulatoryRequirementId = regulatoryRequirementId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }

        public RR_StatusHistoryCreateOptions()
        {
        }

        public int RegulatoryRequirementId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }
}
