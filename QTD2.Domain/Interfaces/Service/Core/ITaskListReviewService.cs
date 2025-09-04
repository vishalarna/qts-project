using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITaskListReviewService : Common.IService<TaskListReview>
    {
        public Task<TaskListReview> GetForCopyAndDeleteAsync(int id);
        public Task<string> GetTitleByIdAsync(int id);
        public Task<List<TaskListReview>> GetAllAsync();
        public Task<List<TaskListReview>> GetTaskListReviewsByIdsAndStatusAsync(List<int> taskListReviewIds, string status);
        public Task<TaskListReview> GeForVMAsync(int id);
    }
}
