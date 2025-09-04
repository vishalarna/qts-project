using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_ActionItemSpecs;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingIssue_ActionItemValidation : Validation<TrainingIssue_ActionItem>, ITrainingIssue_ActionItem_Validation
    {
        public TrainingIssue_ActionItemValidation(IStringLocalizerFactory stringLocalizerFactory)
         : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue_ActionItem>(new TrainingIssue_ActionItem_TrainingIssueIdRequiredSpec(), _validationStringLocalizer["TrainingIssue_ActionItem_TrainingIssueIdRequiredSpec"]));
            AddRule(new ValidationRule<TrainingIssue_ActionItem>(new TrainingIssue_ActionItem_OrderRequiredSpec(), _validationStringLocalizer["TrainingIssue_ActionItem_OrderRequiredSpec"]));
            AddRule(new ValidationRule<TrainingIssue_ActionItem>(new TrainingIssue_ActionItem_ActionItemNameRequiredSpec(), _validationStringLocalizer["TrainingIssue_ActionItem_ActionItemNameRequiredSpec"]));
        }
    }
}
