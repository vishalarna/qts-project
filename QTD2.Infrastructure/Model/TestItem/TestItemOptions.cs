using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class TestItemOptions
    {
        public string ActionType { get; set; }

        public int[] TestIds { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int? EOId { get; set; }
    }
}
