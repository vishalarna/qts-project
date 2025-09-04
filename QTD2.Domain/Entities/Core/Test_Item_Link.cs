using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Test_Item_Link : Entity
    {
        public int TestId { get; set; }

        public int TestItemId { get; set; }

        public int Sequence { get; set; }

        public virtual Test Test { get; set; }

        public virtual TestItem TestItem { get; set; }

        public Test_Item_Link()
        {
        }

        public Test_Item_Link(Test test, TestItem testItem, int sequence = 0)
        {
            Test = test;
            TestId = test.Id;
            TestItem = testItem;
            TestItemId = testItem.Id;
            Sequence = sequence;
        }
    }
}
