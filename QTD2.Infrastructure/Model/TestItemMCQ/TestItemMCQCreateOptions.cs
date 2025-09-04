using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItemMCQ
{
    public class TestItemMCQCreateOptions
    {
        public int TestItemId { get; set; }

        public string ChoiceDescription { get; set; }

        public bool IsCorrect { get; set; }

        public int Number { get; set; }
    }
}
