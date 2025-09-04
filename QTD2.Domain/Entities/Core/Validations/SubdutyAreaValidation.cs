using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SubdutyAreaSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SubdutyAreaValidation : Validation<SubdutyArea>, ISubdutyAreaValidation
    {
        public SubdutyAreaValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SubdutyArea>(new SubdutyAreaNumberRequiredSpec(), _validationStringLocalizer["SubDutyAreaNumRequired"]));
            AddRule(new ValidationRule<SubdutyArea>(new SubdutyAreaDutyAreaIdRequiredSpec(), _validationStringLocalizer["SubDutyAreaDutyAreaIdRequired"]));
        }
    }
}
