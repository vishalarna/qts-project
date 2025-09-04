using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_TopicHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_TopicHistoryValidation : Validation<EnablingObjective_TopicHistory>, IEnablingObjective_TopicHistoryValidation
    {
        public EnablingObjective_TopicHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_TopicHistory>(new EO_TopicHistoryTopicIdRequiredSpec(), _validationStringLocalizer["EO_TopicHistoryTopicIdRequiredSpec"]));
        }
    }
}
