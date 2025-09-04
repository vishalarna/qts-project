using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskListReview_GeneralReviewerRepository : Common.Repository<TaskListReview_GeneralReviewer>, ITaskListReview_GeneralReviewerRepository
    {
        public TaskListReview_GeneralReviewerRepository(QTDContext context)
            : base(context)
        {
        }
    }
}