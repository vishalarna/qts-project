using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestItemMCQ : Entity
    {
        public int TestItemId { get; set; }

        public string ChoiceDescription { get; set; }

        public bool IsCorrect { get; set; }

        public int Number { get; set; }

        public virtual TestItem TestItem { get; set; }

        public TestItemMCQ(int testItemId, string choiceDescription, bool isCorrect, int number)
        {
            TestItemId = testItemId;
            ChoiceDescription = choiceDescription;
            IsCorrect = isCorrect;
            Number = number;
        }

        public TestItemMCQ()
        {
        }
    }
}
