using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SafetyHazard_ILA_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SafetyHazard_ILA_LinkValidation : Validation<SafetyHazard_ILA_Link>, ISafetyHazard_ILA_LinkValidation
    {
        public SafetyHazard_ILA_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SafetyHazard_ILA_Link>(new SafetyHazard_ILA_LinkIlaIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_ILA_LinkIlaIdRequiredSpec"]));
            AddRule(new ValidationRule<SafetyHazard_ILA_Link>(new SafetyHazard_ILA_LinkSHIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_ILA_LinkSHIdRequiredSpec"]));
        }
    }
}
