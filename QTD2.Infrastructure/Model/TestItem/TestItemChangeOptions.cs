using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class TestItemChangeOptions
    {
        public int? EOId { get; set; }

        public string Reason { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
