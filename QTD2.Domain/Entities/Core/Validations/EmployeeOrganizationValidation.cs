using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmployeeOrganizationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmployeeOrganizationValidation : Validation<EmployeeOrganization>, IEmployeeOrganizationValidation
    {
        public EmployeeOrganizationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EmployeeOrganization>(new EmpOrgEmployeeIdRequiredSpec(), _validationStringLocalizer["EmpOrgEmployeeIdRequired"]));
            AddRule(new ValidationRule<EmployeeOrganization>(new EmpOrgOrganizationIdRequiredSpec(), _validationStringLocalizer["EmpOrgOrganizationIdRequired"]));
        }
    }
}
