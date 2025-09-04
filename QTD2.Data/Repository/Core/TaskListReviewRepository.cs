using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskListReviewRepository : Common.Repository<TaskListReview>, ITaskListReviewRepository
    {
        public TaskListReviewRepository(QTDContext context)
            : base(context)
        {
        }
    }
}