using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TaskReview_FindingService : Common.Service<TaskReview_Finding>,
                  ITaskReview_FindingService
    {
        public TaskReview_FindingService(ITaskReview_FindingRepository repository, ITaskReview_FindingValidation validation)
            : base(repository, validation)
        {
        }
    }
}