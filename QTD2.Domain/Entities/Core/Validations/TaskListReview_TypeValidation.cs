using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReview_TypeSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskListReview_TypeValidation : Validation<TaskListReview_Type>, ITaskListReview_TypeValidation
    {
        public TaskListReview_TypeValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskListReview_Type>(new TaskListReview_TypeRequiredSpec(), _validationStringLocalizer["TaskListReview_Type_TypeRequiredSpec"]));
        }
    }
}
