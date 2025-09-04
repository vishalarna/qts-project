using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.RR_StatusHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class RR_StatusHistoryValidation : Validation<RR_StatusHistory>, IRR_StatusHistoryValidation
    {
        public RR_StatusHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<RR_StatusHistory>(new RR_StatusHistoryRRIdRequiredSpec(), _validationStringLocalizer["RR_StatusHistoryRRIdRequiredSpec"]));
        }
    }
}
