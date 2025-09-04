using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_DriverTypeSpecs;
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
    public class TrainingIssue_DriverTypeValidation : Validation<TrainingIssue_DriverType>, ITrainingIssue_DriverType_Validation
    {
        public TrainingIssue_DriverTypeValidation(IStringLocalizerFactory stringLocalizerFactory)
         : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue_DriverType>(new TrainingIssue_DriverTypeRequiredSpec(), _validationStringLocalizer["TrainingIssue_DriverTypeRequiredSpec"]));
        }
    }
}
