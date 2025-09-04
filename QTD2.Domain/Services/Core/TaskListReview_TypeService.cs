using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TaskListReview_TypeService : Common.Service<TaskListReview_Type>,
             ITaskListReview_TypeService
    {
        public TaskListReview_TypeService(ITaskListReview_TypeRepository repository, ITaskListReview_TypeValidation validation)
            : base(repository, validation)
        {
        }
    }
}