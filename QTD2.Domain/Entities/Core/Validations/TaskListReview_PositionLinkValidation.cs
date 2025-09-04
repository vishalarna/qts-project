using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_Evaluator_LinksSpecs;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReview_PositionLinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskListReview_PositionLinkValidation : Validation<TaskListReview_PositionLink>, ITaskListReview_PositionLinkValidation
    {
        public TaskListReview_PositionLinkValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskListReview_PositionLink>(new TaskListReview_PositionLink_TaskListReviewIdRequiredSpec(), _validationStringLocalizer["TaskListReview_PositionLink_TaskListReviewIdRequiredSpec"]));
            AddRule(new ValidationRule<TaskListReview_PositionLink>(new TaskListReview_PositionLink_PositionIdRequiredSpec(), _validationStringLocalizer["TaskListReview_PositionLink_PositionIdRequiredSpec"]));
        }
    }
}
