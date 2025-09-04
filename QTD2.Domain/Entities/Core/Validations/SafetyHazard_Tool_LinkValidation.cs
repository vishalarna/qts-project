using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SafetyHazard_Tool_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SafetyHazard_Tool_LinkValidation : Validation<SafetyHazard_Tool_Link>, ISafetyHazard_Tool_LinkValidation
    {
        public SafetyHazard_Tool_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SafetyHazard_Tool_Link>(new SH_Tool_LinkSHIdRequiredSpec(), _validationStringLocalizer["SH_Tool_LinkSHIdRequried"]));
            AddRule(new ValidationRule<SafetyHazard_Tool_Link>(new SH_Tool_LinkToolIdRequiredSpec(), _validationStringLocalizer["SH_Tool_LinkToolIdRequired"]));
        }
    }
}
