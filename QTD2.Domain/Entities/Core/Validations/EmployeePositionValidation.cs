using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmployeePositionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmployeePositionValidation : Validation<EmployeePosition>, IEmployeePositionValidation
    {
        public EmployeePositionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EmployeePosition>(new EmpPosEmployeeIdRequiredSpec(), _validationStringLocalizer["EmpPosEmployeeIdRequired"]));
            AddRule(new ValidationRule<EmployeePosition>(new EmpPosPositionIdRequiredSpec(), _validationStringLocalizer["EmpPosPositionIdRequired"]));
            AddRule(new ValidationRule<EmployeePosition>(new EmpPosStartDateRequiredSpec(), _validationStringLocalizer["EmpPosStartDateRequired"]));
            AddRule(new ValidationRule<EmployeePosition>(new EmpPosTraineeBitRequiredSpec(), _validationStringLocalizer["EmpPosTraineeValRequired"]));
        }
    }
}
