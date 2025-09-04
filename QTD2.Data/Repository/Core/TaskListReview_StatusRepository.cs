using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskListReview_StatusRepository : Common.Repository<TaskListReview_Status>, ITaskListReview_StatusRepository
    {
        public TaskListReview_StatusRepository(QTDContext context)
            : base(context)
        {
        }
    }
}