using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingTopicSpecs;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingTopicValidation : Validation<TrainingTopic>, ITrainingTopicValidation
    {
        public TrainingTopicValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingTopic>(new TrainingTopicNameRequiredSpec(), _validationStringLocalizer["TrainingTopicNameRequiredSpec"]));
        }
    }
}
