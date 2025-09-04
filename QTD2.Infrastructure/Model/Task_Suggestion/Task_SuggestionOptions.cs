using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Suggestion
{
    public class Task_SuggestionOptions
    {
        public int TaskId { get; set; }

        public string Description { get; set; }

        public Task_SuggestionOptions(int taskId, string description)
        {
            TaskId = taskId;
            Description = description;
        }

        public Task_SuggestionOptions() { }
    }
}
