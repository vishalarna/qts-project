using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class QuestionAndAnswer_ActionItem : ActionItem
    {
        public virtual List<ActionItem_QuestionAndAnswer_Operation> ActionItem_QuestionAndAnswer_Operations { get; set; } = new List<ActionItem_QuestionAndAnswer_Operation>();

        public QuestionAndAnswer_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }
        public QuestionAndAnswer_ActionItem () { }

        public ActionItem_QuestionAndAnswer_Operation AddQuestionAnswerOperation(int operationTypeId, int? questionId, string question, string answer)
        {
            ActionItem_QuestionAndAnswer_Operation quesAnsOperation = new ActionItem_QuestionAndAnswer_Operation(this.Id, operationTypeId, questionId, question, answer);
            AddEntityToNavigationProperty<ActionItem_QuestionAndAnswer_Operation>(quesAnsOperation);
            return quesAnsOperation;
        }

        public void UpdateQuesAnsOperation(int id, int operationTypeId, int? questionId, string question, string answer)
        {
            ActionItem_QuestionAndAnswer_Operation quesAnsOperation = ActionItem_QuestionAndAnswer_Operations.FirstOrDefault(x => x.Id == id);
            if (quesAnsOperation != null)
            {
                quesAnsOperation.UpdateOperation(operationTypeId, questionId, question, answer);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<QuestionAndAnswer_ActionItem>(createdBy);

            // Ensure includes
            copy.ActionItem_QuestionAndAnswer_Operations = new List<ActionItem_QuestionAndAnswer_Operation>();
            foreach (var actionItemQuestionAndAnswerOperation in this.ActionItem_QuestionAndAnswer_Operations)
            {
                var actionItemQuestionAndAnswerOperationCopy = actionItemQuestionAndAnswerOperation.Copy<ActionItem_QuestionAndAnswer_Operation>(createdBy);
                actionItemQuestionAndAnswerOperationCopy.ActionItemId = 0;
                copy.ActionItem_QuestionAndAnswer_Operations.Add(actionItemQuestionAndAnswerOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}
