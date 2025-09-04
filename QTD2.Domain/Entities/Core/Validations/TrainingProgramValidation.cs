using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingProgramSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingProgramValidation : Validation<TrainingProgram>, ITrainingProgramValidation
    {
        public TrainingProgramValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingProgram>(new TrainingProgramPositionIdRequiredSpec(), _validationStringLocalizer["TrainingProgramPositionIdRequried"]));
            //AddRule(new ValidationRule<TrainingProgram>(new TrainingProgramVersionRequiredSpec(), _validationStringLocalizer["TrainingProgramVersionRequired"]));
            AddRule(new ValidationRule<TrainingProgram>(new TrainingProgram_ProgramTypeRequired(), _validationStringLocalizer["TrainingProgram_ProgramTypeRequired"]));
            //AddRule(new ValidationRule<TrainingProgram>(new TrainingProgramStartDateRequired(), _validationStringLocalizer["TrainingProgramStartDateRequired"]));
        }
    }
}
