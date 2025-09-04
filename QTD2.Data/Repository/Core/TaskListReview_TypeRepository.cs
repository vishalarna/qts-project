using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskListReview_TypeRepository : Common.Repository<TaskListReview_Type>, ITaskListReview_TypeRepository
    {
        public TaskListReview_TypeRepository(QTDContext context)
            : base(context)
        {
        }
    }
}