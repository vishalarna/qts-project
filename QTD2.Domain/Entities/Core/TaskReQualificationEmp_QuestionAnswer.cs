using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskReQualificationEmp_QuestionAnswer : Common.Entity
    {
        public int TaskQualificationId { get; set; }
        public int TaskQuestionId { get; set; }
        public string Comments { get; set; }
        public int EvaluatorId { get; set; }
        public DateTime CommentDate { get; set; }
        public int TraineeId { get; set; }
        public bool IsCompleted { get; set; }
        public virtual Task_Question Task_Question { get; set; }
        public virtual Employee Evaluator { get; set; }
        public virtual Employee Trainee { get; set; }

        public virtual TaskQualification TaskQualification { get; set; }


        public TaskReQualificationEmp_QuestionAnswer(int taskQualificationId, int taskQuestionId, string comments, int evaluatorId, DateTime commentDate, int traineeId, bool isCompleted)
        {

            TaskQualificationId = taskQualificationId;
            TaskQuestionId = taskQuestionId;
            Comments = comments;
            EvaluatorId = evaluatorId;
            CommentDate = commentDate;
            TraineeId = traineeId;
            IsCompleted = isCompleted;

        }
        public TaskReQualificationEmp_QuestionAnswer()
        {

        }
    }
}
