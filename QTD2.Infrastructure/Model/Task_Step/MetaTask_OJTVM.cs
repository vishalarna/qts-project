using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Step
{
    public class MetaTask_OJTVM
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string Description { get; set; }

        public int? ParentStepId { get; set; }

        public int? Number { get; set; }

        public bool isCreated { get; set; }
    }

    public class MetaTask_QuestionsVM
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public string Question { get; set; }

        public string Answer { get; set; }

        public int QuestionNumber { get; set; }

        public bool isCreated { get; set; }
    }

    public class MetaTask_SuggestionsVM
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public bool isCreated { get; set; }
    }
}
