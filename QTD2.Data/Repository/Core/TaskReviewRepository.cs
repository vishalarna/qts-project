using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskReviewRepository : Common.Repository<TaskReview>, ITaskReviewRepository
    {
        public TaskReviewRepository(QTDContext context)
            : base(context)
        {
        }
    }
}