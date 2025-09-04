using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test_Item_Link
{
    public class TestItemFilterOptions
    {
        public int TestId { get; set; }

        public List<TestItemFilter> options { get; set; }
    }
}
