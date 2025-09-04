using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskReview_StatusValidation : Validation<TaskReview_Status>, ITaskReview_StatusValidation
    {
        public TaskReview_StatusValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
        }
    }
}