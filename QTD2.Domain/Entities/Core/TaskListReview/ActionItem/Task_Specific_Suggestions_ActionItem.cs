using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Task_Specific_Suggestions_ActionItem : ActionItem
    {
        public virtual List<ActionItem_Suggestion_Operation> ActionItem_Suggestion_Operations { get; set; } = new List<ActionItem_Suggestion_Operation>();

        public Task_Specific_Suggestions_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public Task_Specific_Suggestions_ActionItem() { }

        public ActionItem_Suggestion_Operation AddSuggestionOperation(int operationTypeId, int? taskSuggestionId, string description)
        {
            ActionItem_Suggestion_Operation suggestionOperation = new ActionItem_Suggestion_Operation(this.Id, operationTypeId, taskSuggestionId, description);
            AddEntityToNavigationProperty<ActionItem_Suggestion_Operation>(suggestionOperation);
            return suggestionOperation;
        }

        public void UpdateSuggestionOperation(int id ,int operationTypeId, int? taskSuggestionId, string description)
        {
            ActionItem_Suggestion_Operation suggestionOperation = ActionItem_Suggestion_Operations.FirstOrDefault(x => x.Id == id);
            if (suggestionOperation != null)
            {
                suggestionOperation.UpdateOperation(operationTypeId, taskSuggestionId, description);
            }
        }

        public override void Delete()
        {
            base.Delete();
            ActionItem_Suggestion_Operations?.ToList().ForEach(x => x.Delete());
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<Task_Specific_Suggestions_ActionItem>(createdBy);

            // Ensure includes
            copy.ActionItem_Suggestion_Operations = new List<ActionItem_Suggestion_Operation>();
            foreach (var actionItemSuggestionOperation in ActionItem_Suggestion_Operations)
            {
                var actionItemSuggestionOperationCopy = actionItemSuggestionOperation.Copy<ActionItem_Suggestion_Operation>(createdBy);
                actionItemSuggestionOperationCopy.ActionItemId = 0;
                copy.ActionItem_Suggestion_Operations.Add(actionItemSuggestionOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}
