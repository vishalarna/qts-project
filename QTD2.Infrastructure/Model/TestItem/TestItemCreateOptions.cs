using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class TestItemCreateOptions
    {
        public int TestItemTypeId { get; set; }

        public int TaxonomyId { get; set; }

        public bool isActive { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int? EOId { get; set; }

        public int Number { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
