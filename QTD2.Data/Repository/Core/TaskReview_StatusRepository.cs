using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskReview_StatusRepository : Common.Repository<TaskReview_Status>, ITaskReview_StatusRepository
    {
        public TaskReview_StatusRepository(QTDContext context)
            : base(context)
        {
        }
    }
}