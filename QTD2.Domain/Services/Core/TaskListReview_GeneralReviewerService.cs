using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TaskListReview_GeneralReviewerService : Common.Service<TaskListReview_GeneralReviewer>,
          ITaskListReview_GeneralReviewerService
    {
        public TaskListReview_GeneralReviewerService(ITaskListReview_GeneralReviewerRepository repository, ITaskListReview_GeneralReviewerValidation validation)
            : base(repository, validation)
        {
        }
    }
}