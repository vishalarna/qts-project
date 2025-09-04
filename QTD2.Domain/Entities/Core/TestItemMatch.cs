using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestItemMatch : Entity
    {
        public int TestItemId { get; set; }

        public string ChoiceDescription { get; set; }

        public string MatchDescription { get; set; }

        public char MatchValue { get; set; }

        public char? CorrectValue { get; set; }

        public int Number { get; set; }

        public virtual TestItem TestItem { get; set; }

        public TestItemMatch(int testItemId, string choiceDescription, string matchDescription, char matchValue,int number, char? correctValue)
        {
            TestItemId = testItemId;
            ChoiceDescription = choiceDescription;
            MatchDescription = matchDescription;
            MatchValue = matchValue;
            CorrectValue = correctValue;
            Number = number;
        }

        public TestItemMatch()
        {
        }
    }
}
