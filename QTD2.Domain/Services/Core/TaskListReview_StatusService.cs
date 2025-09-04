using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TaskListReview_StatusService : Common.Service<TaskListReview_Status>,
           ITaskListReview_StatusService
    {
        public TaskListReview_StatusService(ITaskListReview_StatusRepository repository, ITaskListReview_StatusValidation validation)
            : base(repository, validation)
        {
        }
    }
}