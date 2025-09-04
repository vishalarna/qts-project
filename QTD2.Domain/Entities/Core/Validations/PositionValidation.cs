using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.PositionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class PositionValidation : Validation<Position>, IPositionValidation
    {
        public PositionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Position>(new PositionNameRequiredSpec(), _validationStringLocalizer["PositionNameRequired"]));
            AddRule(new ValidationRule<Position>(new PositionNumberRequiredSpec(), _validationStringLocalizer["PositionNumberRequired"]));
            AddRule(new ValidationRule<Position>(new PositionAbbreviationRequiredSpec(), _validationStringLocalizer["PositionAbbreviationRequired"]));
        }
    }
}
