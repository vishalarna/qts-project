using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_EnablingObjective_Question
{
    public class Version_EnablingObjective_QuestionCreateOptions
    {
        public int EOQuestionId { get; set; }

        public int Version_EnablingObjectiveId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
