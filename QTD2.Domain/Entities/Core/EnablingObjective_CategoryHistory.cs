using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_CategoryHistory : Entity
    {
        public int EnablingObjectiveCategoryId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual EnablingObjective_Category EnablingObjective_Category { get; set; }

        public EnablingObjective_CategoryHistory()
        {
        }

        public EnablingObjective_CategoryHistory(int enablingObjectiveCategoryId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            EnablingObjectiveCategoryId = enablingObjectiveCategoryId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }
    }
}
