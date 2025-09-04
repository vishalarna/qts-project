using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SafetyHazard_EO_LinkSpecs;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SafetyHazard_EO_LinkValidation : Validation<SafetyHazard_EO_Link>, ISafetyHazard_EO_LinkValidation
    {
        public SafetyHazard_EO_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SafetyHazard_EO_Link>(new SafetyHazard_EO_LinkSHIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_EO_LinkSHIdRequiredSpec"]));
            AddRule(new ValidationRule<SafetyHazard_EO_Link>(new SafetyHazard_EO_LinkEOIDRequiredSpec(), _validationStringLocalizer["SafetyHazard_EO_LinkEOIDRequiredSpec"]));
        }
    }
}
