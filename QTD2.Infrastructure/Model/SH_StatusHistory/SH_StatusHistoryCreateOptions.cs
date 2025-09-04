using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SH_StatusHistory
{
    public class SH_StatusHistoryCreateOptions
    {
        public int SafetyHazardId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public SH_StatusHistoryCreateOptions()
        {
        }

        public SH_StatusHistoryCreateOptions(int safetyHazardId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            SafetyHazardId = safetyHazardId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }
    }
}
