using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingProgram_HistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingProgram_HistoryValidation : Validation<TrainingProgram_History>, ITrainingProgram_HistoryValidation
    {
        public TrainingProgram_HistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {

            AddRule(new ValidationRule<TrainingProgram_History>(new TrainingProgramHistory_TrainingProgramIdRequiredSpec(), _validationStringLocalizer["TrainingProgramIdRequired"]));
            AddRule(new ValidationRule<TrainingProgram_History>(new TrainingProgramHistory_TrainingProgramVersionIdRequiredSpec(), _validationStringLocalizer["TrainingProgramVersionIdRequired"]));
        }
    }
}
