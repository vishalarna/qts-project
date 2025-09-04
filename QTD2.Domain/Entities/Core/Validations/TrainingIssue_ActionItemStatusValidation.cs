using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_ActionItemStatusSpecs;
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
    public class TrainingIssue_ActionItemStatusValidation : Validation<TrainingIssue_ActionItemStatus>, ITrainingIssue_ActionItemStatus_Validation
    {
        public TrainingIssue_ActionItemStatusValidation(IStringLocalizerFactory stringLocalizerFactory)
         : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue_ActionItemStatus>(new TrainingIssue_ActionItemStatusRequiredSpec(), _validationStringLocalizer["TrainingIssue_ActionItemStatusRequiredSpec"]));
        }
    }
}
