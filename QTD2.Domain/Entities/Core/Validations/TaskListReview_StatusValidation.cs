using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReview_StatusSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskListReview_StatusValidation : Validation<TaskListReview_Status>, ITaskListReview_StatusValidation
    {
        public TaskListReview_StatusValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskListReview_Status>(new TaskListReview_Status_TypeRequiredSpec(), _validationStringLocalizer["TaskListReview_Status_TypeRequiredSpec"]));
        }
    }
}