using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_TestItems : Entity
    {
        public int? Version_EnablingObjectiveId { get; set; }

        public int TestItemId { get; set; }

        public int TestItemTypeId { get; set; }

        public int TaxonomyId { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public string Version_Number { get; set; }

        public virtual TestItem TestItem { get; set; }

        public virtual TestItemType TestItemType { get; set; }

        public virtual TaxonomyLevel TaxonomyLevel { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Image Image { get; set; }

        public Version_TestItems()
        {
        }

        public Version_TestItems(Version_EnablingObjective eo, TestItem testItem,string version_number = "1.0")
        {
            TestItemId = testItem.Id;
            TestItemTypeId = testItem.TestItemTypeId;
            TaxonomyId = testItem.TaxonomyId;
            Version_EnablingObjectiveId = eo.Id;
            IsActive = testItem.IsActive;
            Description = testItem.Description;
            ImageId = testItem.ImageId;
            Version_Number = version_number;
            TestItem = testItem;
        }
    }
}
