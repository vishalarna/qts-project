using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task_Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskReviewActionItemService
    {
        public List<string> GetActionItemTypesAsync();
        public Task<List<TaskReviewActionItem_OperationType_VM>> GetOperationTypesAsync(string actionItemType);
        public Task<TaskReviewActionItem_VM> GetAsync(int id);
        public System.Threading.Tasks.Task DeleteAsync(int id);
        public Task<TaskReviewActionItem_VM> UpdateAsync(int id, TaskReviewActionItem_VM options);
        public Task<ActionItem> CreateActionItemAsync(ActionItem actionItem);
        public Task<ActionItem> UpdateActionItemAsync(ActionItem actionItem);
    }
}
