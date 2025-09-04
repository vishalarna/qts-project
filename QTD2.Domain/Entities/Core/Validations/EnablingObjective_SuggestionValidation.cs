using Microsoft.Extensions.Localization;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_SuggestionSpecs;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_SuggestionValidation : Validation<EnablingObjective_Suggestion>, IEnablingObjective_SuggestionValidation
    {
        public EnablingObjective_SuggestionValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_Suggestion>(new EnablingObjective_Suggestion_EOIdRequiredSpec(), _validationStringLocalizer["EnablingObjective_Suggestion_EOIdRequiredSpec"]));
        }
    }
}
