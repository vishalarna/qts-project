using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TainingProgram_ILA_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingPrograms_ILA_LinkValidation : Validation<TrainingPrograms_ILA_Link>, ITrainingProgram_ILA_LinkValidation
    {
        public TrainingPrograms_ILA_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {

            AddRule(new ValidationRule<TrainingPrograms_ILA_Link>(new TrainingProgram_ILA_LinkILAIdRequiredSpec(), _validationStringLocalizer["TrainingProgramILAIdRequired"]));
            AddRule(new ValidationRule<TrainingPrograms_ILA_Link>(new TrainingProgram_ILA_LinkTrainingProgramIdRequiredSpec(), _validationStringLocalizer["TrainingProgramIdRequired"]));
        }
    }
}
