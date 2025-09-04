using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingProgramSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;


namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingProgramTypeValidation : Validation<TrainingProgramType>, ITrainingProgramTypeValidation
    {
        public TrainingProgramTypeValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
           
            AddRule(new ValidationRule<TrainingProgramType>(new TrainingProgramIdRequiredSpec(), _validationStringLocalizer["TrainingProgramTypeIdRequired"]));
        }
    }
}
