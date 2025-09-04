using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class TestItemCopyOptions
    {
        public List<int> TestItemIds { get; set; } = new List<int>();

        public int TestId { get; set; }
    }
}
