using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestItem_History : Entity
    {
        public int TestItemId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public virtual TestItem TestItem { get; set; }

        public TestItem_History()
        {
        }

        public TestItem_History(int testItemId, string changeNotes, DateTime effectiveDate, bool oldStatus, bool newStatus)
        {
            TestItemId = testItemId;
            ChangeNotes = changeNotes;
            EffectiveDate = effectiveDate;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }
}
