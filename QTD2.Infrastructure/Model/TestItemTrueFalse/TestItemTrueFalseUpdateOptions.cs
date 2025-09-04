using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItemTrueFalse
{
    public class TestItemTrueFalseUpdateOptions
    {
        public int TestItemId { get; set; }

        public string Choices { get; set; }

        public bool IsCorrect { get; set; }
    }
}
