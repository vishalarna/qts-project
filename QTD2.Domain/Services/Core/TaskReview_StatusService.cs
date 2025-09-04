using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TaskReview_StatusService : Common.Service<TaskReview_Status>,
               ITaskReview_StatusService
    {
        public TaskReview_StatusService(ITaskReview_StatusRepository repository, ITaskReview_StatusValidation validation)
            : base(repository, validation)
        {
        }
    }
}