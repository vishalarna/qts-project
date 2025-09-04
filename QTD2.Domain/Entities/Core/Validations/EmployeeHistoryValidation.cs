using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmployeeHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmployeeHistoryValidation : Validation<EmployeeHistory>, IEmployeeHistoryValidation
    {
        public EmployeeHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EmployeeHistory>(new EmployeeHistory_EmpIDRequiredSpec(), _validationStringLocalizer["EmployeeHistory_EmpIDRequiredSpec"]));
        }
    }
}
