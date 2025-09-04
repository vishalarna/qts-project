using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.PositionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
namespace QTD2.Domain.Entities.Core.Validations
{
    public class PositionHistoryValidation : Validation<Position_History>, IPositionHistoryValidation
    {
        public PositionHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Position_History>(new PositionHistoryPositionIdRequiredSpecs(), _validationStringLocalizer["PositionIdRequired"]));

        }
    }

}


