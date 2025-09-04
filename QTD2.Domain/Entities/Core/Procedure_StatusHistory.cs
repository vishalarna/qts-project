using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Procedure_StatusHistory : Entity
    {
        public int ProcedureId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public virtual Procedure Procedure { get; set; }

        public Procedure_StatusHistory()
        {
        }

        public Procedure_StatusHistory(int procedureId, bool oldStatus, bool newStatus, string changeNotes, DateTime changeEffectiveDate)
        {
            ProcedureId = procedureId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeNotes = changeNotes;
            ChangeEffectiveDate = changeEffectiveDate;
        }
    }
}
