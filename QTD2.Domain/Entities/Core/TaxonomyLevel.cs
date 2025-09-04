using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TaxonomyLevel : Entity
    {
        public string Description { get; set; }

        public virtual ICollection<TestItem> TestItems { get; set; } = new List<TestItem>();

        public virtual ICollection<Version_TestItems> Version_TestItems { get; set; } = new List<Version_TestItems>();

        public TaxonomyLevel(string description)
        {
            Description = description;
        }

        public TaxonomyLevel()
        {
        }
    }
}
