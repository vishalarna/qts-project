using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.DutyAreaSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DutyAreaValidation : Validation<DutyArea>, IDutyAreaValidation
    {
        public DutyAreaValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<DutyArea>(new DutyAreaDescriptionRequiredSpec(), _validationStringLocalizer["DutyAreaDescRequired"]));
            AddRule(new ValidationRule<DutyArea>(new DutyAreaNumberRequiredSpec(), _validationStringLocalizer["DutyAreaNumRequired"]));
        }
    }
}
