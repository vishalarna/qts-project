using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskReviewSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class TaskReviewValidation : Validation<TaskReview>, ITaskReviewValidation
    {
        public TaskReviewValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskReview>(new TaskReview_TaskListReviewIDRequiredSpec(), _validationStringLocalizer["TaskReview_TaskListReviewIDRequiredSpec"]));
            AddRule(new ValidationRule<TaskReview>(new TaskReview_TaskIdRequiredSpec(), _validationStringLocalizer["TaskReview_TaskIdRequiredSpec"]));
            AddRule(new ValidationRule<TaskReview>(new TaskReview_StatusIDRequiredSpec(), _validationStringLocalizer["TaskReview_StatusIDRequiredSpec"]));
        }
    }
}