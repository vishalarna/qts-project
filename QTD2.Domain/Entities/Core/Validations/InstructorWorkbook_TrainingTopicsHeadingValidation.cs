using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_TrainingTopicsHeadingSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_TrainingTopicsHeadingValidation : Validation<InstructorWorkbook_TrainingTopicsHeading>, IInstructorWorkbook_TrainingTopicsHeadingValidation
    {
        public InstructorWorkbook_TrainingTopicsHeadingValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_TrainingTopicsHeading>(new InstructorWorkbook_TrainingTopicsHeadingTrainingTopicHeadingRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_TrainingTopicsHeadingTrainingTopicHeadingRequired"]));

        }
    }
}
