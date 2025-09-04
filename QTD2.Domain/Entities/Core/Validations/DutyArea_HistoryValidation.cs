using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.DutyArea_HistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DutyArea_HistoryValidation : Validation<DutyArea_History>, IDutyArea_HistoryValidation
    {
        public DutyArea_HistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DutyArea_History>(new DutyAreaIDRequiredSpecs(), _validationStringLocalizer["DutyAreaIDRequiredSpecs"]));
        }
    }
}
