using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SafetyHazard_Task_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SafetyHazard_Task_LinkValidation : Validation<SafetyHazard_Task_Link>, ISafetyHazard_Task_LinkValidation
    {
        public SafetyHazard_Task_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SafetyHazard_Task_Link>(new SafetyHazard_Task_LinkSHIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_Task_LinkSHIdRequiredSpec"]));
            AddRule(new ValidationRule<SafetyHazard_Task_Link>(new SafetyHazard_Task_LinkTaskIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_Task_LinkTaskIdRequiredSpec"]));
        }
    }
}
