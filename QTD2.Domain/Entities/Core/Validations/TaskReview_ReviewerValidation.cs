using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskReview_ReviewerSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskReview_ReviewerValidation : Validation<TaskReview_Reviewer>, ITaskReview_ReviewerValidation
    {
        public TaskReview_ReviewerValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskReview_Reviewer>(new TaskReview_Reviewer_TaskReviewIDRequiredSpec(), _validationStringLocalizer["TaskReview_Reviewer_TaskReviewIdRequiredSpec"]));
            AddRule(new ValidationRule<TaskReview_Reviewer>(new TaskReview_Reviewer_ReviewerIDRequiredSpec(), _validationStringLocalizer["TaskReview_Reviewer_ReviewerIDRequiredSpec"]));
        }
    }
}