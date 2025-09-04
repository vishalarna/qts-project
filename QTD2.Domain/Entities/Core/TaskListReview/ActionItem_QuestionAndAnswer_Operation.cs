using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_QuestionAndAnswer_Operation : Common.Entity
    {
        public int? ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int? Task_QuestionId { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public virtual QuestionAndAnswer_ActionItem ActionItem { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }
        public virtual Task_Question Task_Question { get; set; }

        public ActionItem_QuestionAndAnswer_Operation(int actionItemid, int operationTypeId, int? task_QuestionId, string question, string answer)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            Task_QuestionId = task_QuestionId;
            Question = question;
            Answer = answer;
        }

        public ActionItem_QuestionAndAnswer_Operation() { }

        public void UpdateOperation(int operationTypeId, int? task_QuestionId, string question, string answer)
        {
            OperationTypeId = operationTypeId;
            Task_QuestionId = task_QuestionId;
            Question = question;
            Answer = answer;
        }

    }
}
