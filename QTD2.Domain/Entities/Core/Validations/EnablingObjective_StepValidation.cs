using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_StepSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_StepValidation : Validation<EnablingObjective_Step>, IEnablingObjective_StepValidation
    {
        public EnablingObjective_StepValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_Step>(new EnablingObjective_StepNumberRequiredSpec(), _validationStringLocalizer["EnablingObjective_StepNumberRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective_Step>(new EnablingObjective_StepDescriptionRequiredSpec(), _validationStringLocalizer["EnablingObjective_StepDescriptionRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective_Step>(new EnablingObjective_StepEOIdRequiredSpec(), _validationStringLocalizer["EnablingObjective_StepEOIdRequiredSpec"]));
        }
    }
}
