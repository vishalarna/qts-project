using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test_Item_Link
{
    public class Test_Item_Link_LinkOptions
    {
        public int TestId { get; set; }

        public int[] TestItemIds { get; set; }

        public bool RandomDistractor { get; set; }

        public bool ItemSeq { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
