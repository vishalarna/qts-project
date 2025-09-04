using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_EmployeeSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DIFSurvey_EmployeeValidation : Validation<DIFSurvey_Employee>, IDIFSurvey_EmployeeValidation
    {
        public DIFSurvey_EmployeeValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DIFSurvey_Employee>(new DIFSurvey_EmployeeDIFSurveryIdRequiredSpec(), _validationStringLocalizer["DIFSurvey_EmployeeDIFSurveryIdRequiredSpec"]));
            AddRule(new ValidationRule<DIFSurvey_Employee>(new DIFSurvey_EmployeeEmployeeIdRequiredSpec(), _validationStringLocalizer["DIFSurvey_EmployeeEmployeeIdRequiredSpec"]));
            AddRule(new ValidationRule<DIFSurvey_Employee>(new DIFSurvey_EmployeeStatusIdRequiredSpec(), _validationStringLocalizer["DIFSurvey_EmployeeStatusIdRequiredSpec"]));
        }
    }
}
