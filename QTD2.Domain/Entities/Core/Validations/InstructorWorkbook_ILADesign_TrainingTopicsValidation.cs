using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_TrainingTopicsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILADesign_TrainingTopicsValidation : Validation<InstructorWorkbook_ILADesign_TrainingTopics>, IInstructorWorkbook_ILADesign_TrainingTopicsValidation
    {
        public InstructorWorkbook_ILADesign_TrainingTopicsValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_TrainingTopics>(new InstructorWorkbook_ILADesign_TrainingTopicsTrainingTopicRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_TrainingTopicsTrainingTopicRequired"]));

        }
    }
}
