using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReview_GeneralReviewerSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskListReview_GeneralReviewerValidation : Validation<TaskListReview_GeneralReviewer>, ITaskListReview_GeneralReviewerValidation
    {
        public TaskListReview_GeneralReviewerValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskListReview_GeneralReviewer>(new TaskListReview_GeneralReviewer_TaskListReviewIdRequiredSpec(), _validationStringLocalizer["TaskListReview_GeneralReviewer_TaskListReviewIdRequiredSpec"]));
            AddRule(new ValidationRule<TaskListReview_GeneralReviewer>(new TaskListReview_GeneralReviewer_GeneralReviewerIdRequiredSpec(), _validationStringLocalizer["TaskListReview_GeneralReviewer_GeneralReviewerIdRequiredSpec"]));
        }
    }
}
