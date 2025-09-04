using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_DriverSubTypeSpecs;
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
    public class TrainingIssue_DriverSubTypeValidation : Validation<TrainingIssue_DriverSubType>, ITrainingIssue_DriverSubType_Validation
    {
        public TrainingIssue_DriverSubTypeValidation(IStringLocalizerFactory stringLocalizerFactory)
         : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue_DriverSubType>(new TrainingIssue_DriverSubType_DriverTypeIdRequiredSpec(), _validationStringLocalizer["TrainingIssue_DriverSubType_DriverTypeIdRequiredSpec"]));
            AddRule(new ValidationRule<TrainingIssue_DriverSubType>(new TrainingIssue_DriverSubType_SubTypeRequiredSpec(), _validationStringLocalizer["TrainingIssue_DriverSubType_SubTypeRequiredSpec"]));
        }
    }
}
