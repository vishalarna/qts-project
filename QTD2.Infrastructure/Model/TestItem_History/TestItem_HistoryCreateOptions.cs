using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem_History
{
    public class TestItem_HistoryCreateOptions
    {
        public int TestItemId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }
    }
}
