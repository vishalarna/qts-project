using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvery_Task_StatusSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DIFSurvey_Task_StatusValidation : Validation<DIFSurvey_Task_Status>, IDIFSurvey_Task_StatusValidation
    {
        public DIFSurvey_Task_StatusValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DIFSurvey_Task_Status>(new DIFSurvey_Task_StatusStatusRequiredSpec(), _validationStringLocalizer["DIFSurvery_Task_StatusStatusRequiredSpec"]));
        }
    }
}