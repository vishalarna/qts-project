using QTD2.Domain.Entities.Core;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITaskReviewService : Common.IService<TaskReview>
    {
        public Task<TaskReview> GetTaskReviewWithAllIncludeAsync(int id);
        public Task<List<TaskReview>> GetTaskReviewsByIdsAsync(List<int> taskReviewIds);
        public Task<List<TaskReview>> GetTaskReviewsAsync(List<int> taskReviewIds);
    }
}
