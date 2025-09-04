using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_CategoryHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_CategoryHistoryValidation : Validation<EnablingObjective_CategoryHistory>, IEnablingObjective_CategoryHistoryValidation
    {
        public EnablingObjective_CategoryHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_CategoryHistory>(new EO_CategoryHistoryEOIdRequiredSpec(), _validationStringLocalizer["EO_CategoryHistoryEOIdRequiredSpec"]));
        }
    }
}
