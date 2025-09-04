using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILAHistory : Entity
    {
        public int ILAId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual ILA ILA { get; set; }

        public ILAHistory()
        {
        }

        public ILAHistory(int iLAId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            ILAId = iLAId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }
    }
}
