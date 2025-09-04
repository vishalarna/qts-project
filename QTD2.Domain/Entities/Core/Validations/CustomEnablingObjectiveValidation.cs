using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CustomEnablingObjectiveSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class CustomEnablingObjectiveValidation : Validation<CustomEnablingObjective>, ICustomEnablingObjectiveValidation
    {
        public CustomEnablingObjectiveValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<CustomEnablingObjective>(new Custom_EO_EOTopicIdRequiredSpec(), _validationStringLocalizer["Custom_EO_EOTopicIdRequired"]));
            //AddRule(new ValidationRule<CustomEnablingObjective>(new Custom_EO_DescriptionRequiredSpec(), _validationStringLocalizer["Custom_EO_DescriptionRequired"]));
        }
    }
}
