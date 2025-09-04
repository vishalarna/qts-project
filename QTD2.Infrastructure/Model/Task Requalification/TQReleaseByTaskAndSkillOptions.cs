using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TQReleaseByTaskAndSkillOptions
    {
        public int TaskId { get; set; }
        public int EnablingObjectiveId { get; set; }

        public int[] EmpIds { get; set; }

        public ICollection<EvaluatorOptions> EvaluatorOptions { get; set; } = new List<EvaluatorOptions>();

        public int? EvalMethodId { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public bool OneReq { get; set; }

        public int MultiReq { get; set; }

        public bool OrderMatters { get; set; }

        public bool ShowSuggestions { get; set; }

        public bool ShowQuestions { get; set; }
    }
}
