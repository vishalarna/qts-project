using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmployeeSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmployeeValidation : Validation<Employee>, IEmployeeValidation
    {
        public EmployeeValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Employee>(new PersonIdRequiredSpec(), _validationStringLocalizer["EmployeePersonIdRequired"]));
        }
    }
}
