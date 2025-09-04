using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjectiveHistory : Entity
    {
        public int EnablingObjectiveId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public int Version_EnablingObjectiveId { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public EnablingObjectiveHistory()
        {
        }

        public EnablingObjectiveHistory(int enablingObjectiveId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            EnablingObjectiveId = enablingObjectiveId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }
    }
}
