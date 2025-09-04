using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_TestItem : TrainingIssue_DataElement
    {
        public int? TestItemId { get; set; }
        public TestItem TestItem { get; set; }

        public DataElement_TestItem(int trainingIssueId, int? testItemId) : base(trainingIssueId)
        {
            TestItemId = testItemId;
        }
        public DataElement_TestItem() { }
    }
}
