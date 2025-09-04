using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SafetyHazard_CategoryHistory : Entity
    {
        public SafetyHazard_CategoryHistory(int safetyHazardCategoryId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            SafetyHazardCategoryId = safetyHazardCategoryId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }

        public SafetyHazard_CategoryHistory()
        {
        }

        public int SafetyHazardCategoryId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual SaftyHazard_Category SafetyHazard_Category { get; set; }
    }
}
