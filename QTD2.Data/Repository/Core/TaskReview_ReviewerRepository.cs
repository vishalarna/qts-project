using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskReview_ReviewerRepository : Common.Repository<TaskReview_Reviewer>, ITaskReview_ReviewerRepository
    {
        public TaskReview_ReviewerRepository(QTDContext context)
            : base(context)
        {
        }
    }
}