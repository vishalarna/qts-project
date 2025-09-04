using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EnablingObjective_Question
{
    public class EnablingObjective_QuestionCreateOptions
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        public bool IsSignificant { get; set; }

        public int QuestionNumber { get; set; }
    }
}
