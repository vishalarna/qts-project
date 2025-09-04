using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_ActionItemPrioritySpecs;
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
    public class TrainingIssue_ActionItemPriorityValidation : Validation<TrainingIssue_ActionItemPriority>, ITrainingIssue_ActionItemPriority_Validation
    {
        public TrainingIssue_ActionItemPriorityValidation(IStringLocalizerFactory stringLocalizerFactory)
         : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue_ActionItemPriority>(new TrainingIssue_ActionItemPriorityRequiredSpec(), _validationStringLocalizer["TrainingIssue_ActionItemPriorityRequiredSpec"]));
        }
    }
}
