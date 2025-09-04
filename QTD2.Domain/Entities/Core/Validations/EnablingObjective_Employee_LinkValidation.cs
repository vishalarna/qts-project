using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_Employee_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_Employee_LinkValidation : Validation<EnablingObjective_Employee_Link>, IEnablingObjective_Employee_LinkValidation
    {
        public EnablingObjective_Employee_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_Employee_Link>(new EnablingObjective_Employee_LinkEOIdRequiredSpec(), _validationStringLocalizer["EnablingObjective_Employee_LinkEOIdRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective_Employee_Link>(new EnablingObjective_Employee_LinkEmployeeIdRequiredSpec(), _validationStringLocalizer["EnablingObjective_Employee_LinkEmployeeIdRequiredSpec"]));
        }
    }
}
