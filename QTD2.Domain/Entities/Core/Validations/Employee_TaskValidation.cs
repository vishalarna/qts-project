using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Employee_TaskSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Employee_TaskValidation : Validation<Employee_Task>, IEmployee_TaskValidation
    {
        public Employee_TaskValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Employee_Task>(new Employee_TaskEmployeeIdRequiredSpec(), _validationStringLocalizer["Employee_TaskEmployeeIdRequired"]));
            AddRule(new ValidationRule<Employee_Task>(new Employee_Task_TaskIdRequiredSpec(), _validationStringLocalizer["Employee_Task_TaskIdRequired"]));
            AddRule(new ValidationRule<Employee_Task>(new Employee_TaskMajorVersionRequiredSpec(), _validationStringLocalizer["Employee_TaskMajorVersionRequired"]));
            AddRule(new ValidationRule<Employee_Task>(new Employee_TaskMinorVersionRequiredSpec(), _validationStringLocalizer["Employee_TaskMinorVersionRequired"]));
        }
    }
}
