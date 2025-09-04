using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class Employee_ScormUploadValidation : Validation<Employee_ScormUpload>, IEmployee_ScormUploadValidation
    {
        public Employee_ScormUploadValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
           // AddRule(new ValidationRule<Employee_ScormUpload>(new Employee_ScormUpload_EmployeeIdRequired(), _validationStringLocalizer["Employee_ScormUpload_EmployeeIdRequired"]));
        }
    }
}
