using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SafetyHazard_Set_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SafetyHazard_Set_LinkValidation : Validation<SafetyHazard_Set_Link>, ISafetyHazard_Set_LinkValidation
    {
        public SafetyHazard_Set_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SafetyHazard_Set_Link>(new SafetyHazard_Set_LinkSHZIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_Set_LinkSHZIdRequiredSpec"]));
            AddRule(new ValidationRule<SafetyHazard_Set_Link>(new SafetyHazard_Set_LinkSHZSetIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_Set_LinkSHZSetIdRequiredSpecs"]));
        }
    }
}
