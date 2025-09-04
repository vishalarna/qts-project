using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Position_TaskSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Position_TaskValidation : Validation<Position_Task>, IPosition_TaskValidation
    {
        public Position_TaskValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Position_Task>(new PositionTaskPositionIdRequiredSpecs(), _validationStringLocalizer["PositionIdRequired"]));
            AddRule(new ValidationRule<Position_Task>(new PositionTask_TaskIdRequiredSpecs(), _validationStringLocalizer["TaskIdRequired"]));
            AddRule(new ValidationRule<Position_Task>(new R5ImpactedTaskPositionTaskIdRequiredSpecs(), _validationStringLocalizer["PositionTaskIdRequired"]));
            AddRule(new ValidationRule<Position_Task>(new R5ImpactedTaskImpactedTaskIdRequiredSpecs(), _validationStringLocalizer["ImpactedTaskIdRequired"]));
        }
    }
}
