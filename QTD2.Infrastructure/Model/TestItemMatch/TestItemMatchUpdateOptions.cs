using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItemMatch
{
    public class TestItemMatchUpdateOptions
    {
        public int TestItemId { get; set; }

        public string ChoiceDescription { get; set; }

        public string MatchDescription { get; set; }

        public char MatchValue { get; set; }

        public char? CorrectValue { get; set; }
    }
}
