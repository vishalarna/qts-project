using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SaftyHazard_RR_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SaftyHazard_RR_LinkValidation : Validation<SaftyHazard_RR_Link>, ISaftyHazard_RR_LinkValidation
    {
        public SaftyHazard_RR_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SaftyHazard_RR_Link>(new SaftyHazard_RR_LinkSHIdRequiredSpecs(), _validationStringLocalizer["SafetyHazardIdRequiredSpec"]));
            AddRule(new ValidationRule<SaftyHazard_RR_Link>(new SaftyHazard_RR_LinkRRIdRequiredSpecs(), _validationStringLocalizer["RegulatoryRequirementIdRequiredSpec"]));
        }
    }
}
