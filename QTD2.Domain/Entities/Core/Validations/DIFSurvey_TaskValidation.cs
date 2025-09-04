using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_TaskSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class DIFSurvey_TaskValidation : Validation<DIFSurvey_Task>, IDIFSurvey_TaskValidation
    {
        public DIFSurvey_TaskValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DIFSurvey_Task>(new DIFSurvey_TaskDifSurveyIdRequiredSpec(), _validationStringLocalizer["DIFSurvey_TaskDifSurveyIdRequiredSpec"]));
            AddRule(new ValidationRule<DIFSurvey_Task>(new DIFSurvey_TaskTaskIdRequiredSpec(), _validationStringLocalizer["DIFSurvey_TaskTaskIdRequiredSpec"]));
            AddRule(new ValidationRule<DIFSurvey_Task>(new DIFSurvey_TaskTrainingStatus_CalculatedIdRequiredSpec(), _validationStringLocalizer["DIFSurvey_TaskTrainingStatus_CalculatedIdRequiredSpec"]));
        }
    }
}