using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Position_SQSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Position_SQValidation : Validation<Positions_SQ>, IPositions_SQValidation
    {
        public Position_SQValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Positions_SQ>(new PositionSQPositionIdRequiredSpecs(), _validationStringLocalizer["PositionIdRequired"]));
            AddRule(new ValidationRule<Positions_SQ>(new PositionSQEoIdRequiredSpecs(), _validationStringLocalizer["EOIdRequired"]));
        }
    }
}
