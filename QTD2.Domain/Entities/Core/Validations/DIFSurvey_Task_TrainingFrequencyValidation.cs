using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_Task_TrainingFrequencySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DIFSurvey_Task_TrainingFrequencyValidation : Validation<DIFSurvey_Task_TrainingFrequency>, IDIFSurvey_Task_TrainingFrequencyValidation
    {
        public DIFSurvey_Task_TrainingFrequencyValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DIFSurvey_Task_TrainingFrequency>(new DIFSurvey_Task_TrainingFrequencySpec(), _validationStringLocalizer["DIFSurvey_Task_TrainingFrequencySpec"]));
        }
    }
}