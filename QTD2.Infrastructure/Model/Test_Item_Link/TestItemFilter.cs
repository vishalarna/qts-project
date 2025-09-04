using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test_Item_Link
{
    public class TestItemFilter
    {
        public int TestId { get; set; }

        public int? EOId { get; set; }

        public int[] TestItemTypeIds { get; set; }

        public int[] TaxonomyIds { get; set; }

        public int Generate_number { get; set; }
    }
}
