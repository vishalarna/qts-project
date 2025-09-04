using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_Employee_StatusSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DIFSurvey_Employee_StatusValidation : Validation<DIFSurvey_Employee_Status>, IDIFSurvey_Employee_StatusValidation
    {
        public DIFSurvey_Employee_StatusValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DIFSurvey_Employee_Status>(new DIFSurvey_Employee_StatusRequiredSpec(), _validationStringLocalizer["DIFSurvey_Employee_StatusRequiredSpec"]));
        }
    }
}
