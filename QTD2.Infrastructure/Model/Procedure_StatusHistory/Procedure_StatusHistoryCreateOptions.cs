using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Procedure_StatusHistory
{
    public class Procedure_StatusHistoryCreateOptions
    {
        public Procedure_StatusHistoryCreateOptions()
        {
        }

        public Procedure_StatusHistoryCreateOptions(int[] procedureIds, bool oldStatus, bool newStatus, string changeNotes, DateTime changeEffectiveDate)
        {
            ProcedureIds = procedureIds;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeNotes = changeNotes;
            ChangeEffectiveDate = changeEffectiveDate;
        }

        public int[] ProcedureIds { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }
    }
}
