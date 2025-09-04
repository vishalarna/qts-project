using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmployeeDocumentSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmployeeDocumentValidation : Validation<EmployeeDocument>, IEmployeeDocumentValidation
    {
        public EmployeeDocumentValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        { 
            AddRule(new ValidationRule<EmployeeDocument>(new EmployeeDocumentSpecs_EmpIDRequiredSpecs(), _validationStringLocalizer["EmployeeDocumentSpecs_EmpIDRequiredSpecs"]));
        }
    }
}
