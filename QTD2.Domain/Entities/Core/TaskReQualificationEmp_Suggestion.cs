using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskReQualificationEmp_Suggestion : Common.Entity
    {
        public int TaskQualificationId { get; set; }
        public int TaskSuggestionId { get; set; }
        public string Comments { get; set; }
        public int EvaluatorId { get; set; }
        public DateTime CommentDate { get; set; }
        public int TraineeId { get; set; }
        public bool IsCompleted { get; set; }
        public virtual Task_Suggestion Task_Suggestion { get; set; }
        public virtual Employee Evaluator { get; set; }
        public virtual Employee Trainee { get; set; }

        public virtual TaskQualification TaskQualification { get; set; }

      
        public TaskReQualificationEmp_Suggestion(int taskQualificationId, int taskSuggestionId, string comments, int evaluatorId, DateTime commentDate, int traineeId, bool isCompleted)
        {
  
            TaskQualificationId = taskQualificationId;
            TaskSuggestionId = taskSuggestionId;
            Comments = comments;
            EvaluatorId = evaluatorId;
            CommentDate = commentDate;
            TraineeId = traineeId;
            IsCompleted = isCompleted;

        }
    }
}
