using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestItem : Entity
    {
        public int TestItemTypeId { get; set; }

        public int TaxonomyId { get; set; }

        public int? EOId { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public string Number { get; set; }

        public int? ImageId { get; set; }

        public virtual TestItemType TestItemType { get; set; }

        public virtual TaxonomyLevel TaxonomyLevel { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public virtual ICollection<TestItemTrueFalse> TestItemTrueFalses { get; set; } = new List<TestItemTrueFalse>();

        public virtual ICollection<TestItemFillBlank> TestItemFillBlanks { get; set; } = new List<TestItemFillBlank>();

        public virtual ICollection<TestItemMatch> TestItemMatches { get; set; } = new List<TestItemMatch>();

        public virtual ICollection<TestItemMCQ> TestItemMCQs { get; set; } = new List<TestItemMCQ>();

        public virtual ICollection<TestItemShortAnswer> TestItemShortAnswers { get; set; } = new List<TestItemShortAnswer>();

        public virtual ICollection<Test_Item_Link> Test_Item_Links { get; set; } = new List<Test_Item_Link>();

        public virtual ICollection<TestItem_History> TestItem_Histories { get; set; } = new List<TestItem_History>();

        public virtual ICollection<Version_TestItems> Version_TestItems { get; set; } = new List<Version_TestItems>();

        public virtual Image Image { get; set; }

        public TestItem()
        {
        }

        public TestItem(int testItemTypeId, int taxonomyId, bool isActive, string description, string number, int? eOId)
        {
            TestItemTypeId = testItemTypeId;
            TaxonomyId = taxonomyId;
            IsActive = isActive;
            Description = description;
            EOId = eOId;
            Number = number;
        }

        public override void Delete()
        {
            base.Delete();

            if(Test_Item_Links != null)
            {
                foreach(var testItemLink in Test_Item_Links)
                {
                    testItemLink.Delete();
                }
            }
        }
    }
}
