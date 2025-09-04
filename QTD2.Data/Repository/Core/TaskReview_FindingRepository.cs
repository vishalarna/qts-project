using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskReview_FindingRepository : Common.Repository<TaskReview_Finding>, ITaskReview_FindingRepository
    {
        public TaskReview_FindingRepository(QTDContext context)
            : base(context)
        {
        }
    }
}