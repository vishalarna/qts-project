using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
   public class ActionItemService : Common.Service<ActionItem>,
                  IActionItemService
    {
        public ActionItemService(IActionItemRepository repository, IActionItemValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<ActionItem> GetActionItemWithIncludeAsync(int id)
        {
            var actionItem = await GetWithIncludeAsync(id, new string[] { "Priority", "Assignee.Person", "TaskReview",
                  "ActionItem_SubDuty_Operations.OperationType",
                  "ActionItem_Step_Operations.OperationType",
                  "ActionItem_QuestionAndAnswer_Operations.OperationType",
                  "ActionItem_Suggestion_Operations.OperationType",
                  "ActionItem_EnablingObjective_Operations.OperationType",
                  "ActionItem_Tool_Operations.OperationType",
                  "ActionItem_RegulatoryRequirement_Operations.OperationType",
                  "ActionItem_Procedure_Operations.OperationType",
                  "ActionItem_SafetyHazard_Operations.OperationType" });

            return actionItem;
        }

        public async Task<List<ActionItem>> GetActionItemsByTaskReviewIds(List<int> taskReviewIds)
        {
            List<Expression<Func<Domain.Entities.Core.ActionItem, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.ActionItem, bool>>>();
            predicates.Add(x => taskReviewIds.Contains(x.TaskReviewId));
            return (await FindWithIncludeAsync(predicates, new string[] { "Assignee.Person", "Priority" },true)).ToList();
        }
    }
}