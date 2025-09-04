using System;

namespace QTD2.Infrastructure.Model.Procedure
{
    public class ProcedureOptions
    {
        public bool isSignificant { get; set; }

        public string ActionType { get; set; }

        public DateTime? ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public int[] procedureIds { get; set; }

        public int ProcedureId { get; set; }
    }
}
