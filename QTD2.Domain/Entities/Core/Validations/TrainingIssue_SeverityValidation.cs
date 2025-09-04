using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_SeverityRequiredSpecs;
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
    public class TrainingIssue_SeverityValidation : Validation<TrainingIssue_Severity>, ITrainingIssue_Severity_Validation
    {
        public TrainingIssue_SeverityValidation(IStringLocalizerFactory stringLocalizerFactory)
          : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue_Severity>(new TrainingIssue_SeverityRequiredSpec(), _validationStringLocalizer["TrainingIssue_SeverityRequiredSpec"]));
        }
    }
}
