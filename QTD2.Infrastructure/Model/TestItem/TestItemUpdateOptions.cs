using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class TestItemUpdateOptions
    {
        public int TestItemTypeId { get; set; }

        public int TaxonomyId { get; set; }

        public bool isActive { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int EOId { get; set; }
    }
}
