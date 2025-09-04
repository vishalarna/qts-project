using System;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Proc_IssuingAuthority_History : Entity
    {

        public int ProcedureIssuingAuthorityId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual Procedure_IssuingAuthority Procedure_IssuingAuthority { get; set; }


        public Proc_IssuingAuthority_History(int procedureIssuingAuthorityId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            ProcedureIssuingAuthorityId = procedureIssuingAuthorityId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }

        public Proc_IssuingAuthority_History()
        {
        }

    }
}
