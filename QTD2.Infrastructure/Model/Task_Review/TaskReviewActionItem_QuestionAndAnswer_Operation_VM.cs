using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReviewActionItem_QuestionAndAnswer_Operation_VM
    {
        public int Id { get; set; }
        public int OperationTypeId { get; set; }
        public int? Task_QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Operation { get; set; }

        public TaskReviewActionItem_QuestionAndAnswer_Operation_VM(int id, int operationId, int? task_QuestionId, string question, string answer, string operation)
        {
            Id = id;
            OperationTypeId = operationId;
            Task_QuestionId = task_QuestionId;
            Question = question;
            Answer = answer;
            Operation = operation;
        }
        public TaskReviewActionItem_QuestionAndAnswer_Operation_VM(){}
    }
}
