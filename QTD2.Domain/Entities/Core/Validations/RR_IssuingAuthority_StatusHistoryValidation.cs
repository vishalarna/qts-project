using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.RR_IssuingAuthority_StatusHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class RR_IssuingAuthority_StatusHistoryValidation : Validation<RR_IssuingAuthority_StatusHistory>, IRR_IssuingAuthority_StatusHistoryValidation
    {
        public RR_IssuingAuthority_StatusHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<RR_IssuingAuthority_StatusHistory>(new RR_IssuingAuthority_StatusHistoryRRIAIdRequiredSpec(), _validationStringLocalizer["RR_IssuingAuthority_StatusHistoryRRIAIdRequiredSpec"]));
        }
    }
}
