using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TaskReview_ReviewerService : Common.Service<TaskReview_Reviewer>,
                 ITaskReview_ReviewerService
    {
        public TaskReview_ReviewerService(ITaskReview_ReviewerRepository repository, ITaskReview_ReviewerValidation validation)
            : base(repository, validation)
        {
        }
    }
}