using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestItemShortAnswer : Entity
    {
        public int TestItemId { get; set; }

        public string Responses { get; set; }

        public bool IsCaseSensitive { get; set; }

        public int AcceptableResponses { get; set; }

        public int Number { get; set; }

        public virtual TestItem TestItem { get; set; }

        public TestItemShortAnswer()
        {
        }

        public TestItemShortAnswer(int testItemId, string responses, bool isCaseSensitive, int acceptableResponses, int number)
        {
            TestItemId = testItemId;
            Responses = responses;
            IsCaseSensitive = isCaseSensitive;
            AcceptableResponses = acceptableResponses;
            Number = number;
        }
    }
}
