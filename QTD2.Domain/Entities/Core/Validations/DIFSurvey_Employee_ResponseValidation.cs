using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_Employee_ResponseSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DIFSurvey_Employee_ResponseValidation : Validation<DIFSurvey_Employee_Response>, IDIFSurvey_Employee_ResponseValidation
    {
        public DIFSurvey_Employee_ResponseValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DIFSurvey_Employee_Response>(new DIFSurvey_Employee_ResponseDIFSurvey_EmployeeIdRequiredSpec(), _validationStringLocalizer["DIFSurvey_Employee_ResponseDIFSurvey_EmployeeIdRequiredSpec"]));
            AddRule(new ValidationRule<DIFSurvey_Employee_Response>(new DIFSurvey_Employee_ResponseDIFSurvey_TaskIdRequiredSpec(), _validationStringLocalizer["DIFSurvey_Employee_ResponseDIFSurvey_TaskIdRequiredSpec"]));
        }
    }
}
