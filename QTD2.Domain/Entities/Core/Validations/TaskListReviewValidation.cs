using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvery_DevStatusSpecs;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReviewSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskListReviewValidation : Validation<TaskListReview>, ITaskListReviewValidation
    {
        public TaskListReviewValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskListReview>(new TaskListReview_TitleRequiredSpec(), _validationStringLocalizer["TaskListReview_TitleRequiredSpec"]));
            AddRule(new ValidationRule<TaskListReview>(new TaskListReview_TypeIdRequiredSpec(), _validationStringLocalizer["TaskListReview_TypeIdRequiredSpec"]));
            AddRule(new ValidationRule<TaskListReview>(new TaskListReview_StartDateRequiredSpec(), _validationStringLocalizer["TaskListReview_StartDateRequiredSpec"]));
            AddRule(new ValidationRule<TaskListReview>(new TaskListReview_EndDateRequiredSpec(), _validationStringLocalizer["TaskListReview_EndDateRequiredSpec"]));
            AddRule(new ValidationRule<TaskListReview>(new TaskListReview_StatusIdRequiredSpec(), _validationStringLocalizer["TaskListReview_StatusIdRequiredSpec"]));
           
        }
    }
}
