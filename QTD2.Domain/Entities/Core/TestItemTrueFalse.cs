using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestItemTrueFalse : Entity
    {
        public int TestItemId { get; set; }

        public string Choices { get; set; }

        public bool IsCorrect { get; set; }

        public virtual TestItem TestItem { get; set; }

        public TestItemTrueFalse(int testItemId, string choices, bool correct)
        {
            TestItemId = testItemId;
            Choices = choices;
            IsCorrect = correct;
        }

        public TestItemTrueFalse()
        {
        }
    }
}
