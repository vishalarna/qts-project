using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test_Item_Link
{
    public class TestItemLinkVM
    {
        public int TestItemLinkId { get; set; }
        public int TestItemId { get; set; }
        public int TestItemTypeId { get; set; }
        public int TaxonomyLevelId { get; set; }
        public int? EnablingObjectiveId { get; set; }
        public int Sequence { get; set; }
        public string Number { get; set; }
        public string Question { get; set; }
        public string TestItemType { get; set; }
        public string TaxonomyLevel { get; set; }

        public TestItemLinkVM(){}

        public TestItemLinkVM(int testItemLinkId,int testItemId,int testItemTypeId,int taxonomyLevelId,int? enablingObjectiveId,int sequence,string number,string question,string testItemType,string taxonomyLevel)
        {
            TestItemLinkId = testItemLinkId;
            TestItemId = testItemId;
            TestItemTypeId = testItemTypeId;
            TaxonomyLevelId = taxonomyLevelId;
            EnablingObjectiveId = enablingObjectiveId;
            Sequence = sequence;
            Number = number;
            Question = question;
            TestItemType = testItemType;
            TaxonomyLevel = taxonomyLevel;
        }
    }

}
