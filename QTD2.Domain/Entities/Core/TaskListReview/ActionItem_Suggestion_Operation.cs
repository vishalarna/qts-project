using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_Suggestion_Operation : Common.Entity
    {
        public int ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int? TaskSuggestionId { get; set; }
        public string? Description { get; set; }
        public virtual Task_Specific_Suggestions_ActionItem ActionItem { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }
        public virtual Task_Suggestion Task_Suggestion { get; set; }

        public ActionItem_Suggestion_Operation(int actionItemid, int operationTypeId, int? taskSuggestionId, string description)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            TaskSuggestionId = taskSuggestionId;
            Description = description;
        }

        public ActionItem_Suggestion_Operation() { }
        
        public void UpdateOperation(int operationTypeId, int? taskSuggestionId, string description)
        {
            OperationTypeId = operationTypeId;
            TaskSuggestionId = taskSuggestionId;
            Description = description;
        }
    }
}
