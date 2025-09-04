using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SafetyHazard_CategoryHistorySpecs.cs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SafetyHazard_CategoryHistoryValidation : Validation<SafetyHazard_CategoryHistory>, ISafetyHazard_CategoryHistoryValidation
    {
        public SafetyHazard_CategoryHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SafetyHazard_CategoryHistory>(new SafetyHazard_CategoryHistorySHCatIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_CategoryHistorySHCatIdRequiredSpec"]));
        }
    }
}
