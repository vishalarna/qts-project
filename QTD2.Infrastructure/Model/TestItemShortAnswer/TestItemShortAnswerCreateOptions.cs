using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItemShortAnswer
{
    public class TestItemShortAnswerCreateOptions
    {
        public int TestItemId { get; set; }

        public string Responses { get; set; }

        public bool IsCaseSensitive { get; set; }

        public int AcceptableResponses { get; set; }

        public int Number { get; set; }
    }
}
