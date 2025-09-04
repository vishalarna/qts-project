using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SafetyHazard_History : Entity
    {
        public SafetyHazard_History(int safetyHazardId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            SafetyHazardId = safetyHazardId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }

        public SafetyHazard_History()
        {
        }

        public int SafetyHazardId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual SaftyHazard SafetyHazard { get; set; }
    }
}
