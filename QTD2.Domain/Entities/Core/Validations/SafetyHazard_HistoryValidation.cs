using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SafetyHazard_HistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SafetyHazard_HistoryValidation : Validation<SafetyHazard_History>, ISafetyHazard_HistoryValidation
    {
        public SafetyHazard_HistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SafetyHazard_History>(new SafetyHazard_HistorySHIdRequiredSpec(), _validationStringLocalizer["SafetyHazard_HistorySHIdRequiredSpec"]));
        }
    }
}
