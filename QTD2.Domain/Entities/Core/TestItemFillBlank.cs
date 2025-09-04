using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestItemFillBlank : Entity
    {
        public int TestItemId { get; set; }

        public int CorrectIndex { get; set; }

        public string Correct { get; set; }

        public virtual TestItem TestItem { get; set; }

        public TestItemFillBlank(int testItemId, int correctIndex, string correct)
        {
            TestItemId = testItemId;
            CorrectIndex = correctIndex;
            Correct = correct;
        }

        public TestItemFillBlank()
        {
        }
    }
}
