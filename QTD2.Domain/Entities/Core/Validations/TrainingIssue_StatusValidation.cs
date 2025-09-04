using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_StatusSpecs;
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
    public class TrainingIssue_StatusValidation : Validation<TrainingIssue_Status>, ITrainingIssue_Status_Validation
    {
        public TrainingIssue_StatusValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue_Status>(new TrainingIssue_StatusRequiredSpec(), _validationStringLocalizer["TrainingIssue_StatusRequiredSpec"]));
        }
    }
}
