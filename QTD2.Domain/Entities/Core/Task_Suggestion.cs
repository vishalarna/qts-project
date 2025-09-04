using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Task_Suggestion : Common.Entity
    {
        public int TaskId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }
        
        public int? TaskSuggestionTypeId { get; set; }

        public virtual Task Task { get; set; }

        public virtual ICollection<Version_Task_Suggestion> Version_Task_Suggestions { get; set; } = new List<Version_Task_Suggestion>();

        public virtual ICollection<TaskReQualificationEmp_Suggestion> TaskReQualificationEmp_Suggestions { get; set; } = new List<TaskReQualificationEmp_Suggestion>();

        public virtual Task_SuggestionTypes TaskSuggestionType { get; set; }
        public Task_Suggestion()
        {
        }

        public Task_Suggestion(int taskId, string description, int number)
        {
            TaskId = taskId;
            Description = description;
            Number = number;
        }

        public Task_Suggestion(int taskId, string description, int number,int taskSuggestionTypeId)
        {
            TaskId = taskId;
            Description = description;
            Number = number;
            TaskSuggestionTypeId = taskSuggestionTypeId;
        }
    }
}
