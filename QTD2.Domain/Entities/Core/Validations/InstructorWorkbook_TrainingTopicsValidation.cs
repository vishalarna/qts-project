using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_TrainingTopicsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_TrainingTopicsValidation : Validation<InstructorWorkbook_TrainingTopics>, IInstructorWorkbook_TrainingTopicsValidation
    {
        public InstructorWorkbook_TrainingTopicsValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_TrainingTopics>(new InstructorWorkbook_TrainingTopicsTTHIDRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_TrainingTopicsTTHIDRequired"]));
            AddRule(new ValidationRule<InstructorWorkbook_TrainingTopics>(new InstructorWorkbook_TrainingTopicsTrainingTopicRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_TrainingTopicsTrainingTopicRequired"]));

        }
    }
}
