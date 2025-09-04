using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmployeeCertificationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmployeeCertificationValidation : Validation<EmployeeCertification>, IEmployeeCertificationValidation
    {
        public EmployeeCertificationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EmployeeCertification>(new EmpCertEmployeeIdRequiredSpec(), _validationStringLocalizer["EmpCertEmployeeIdRequired"]));
            AddRule(new ValidationRule<EmployeeCertification>(new EmpCertCertificationIdRequiredSpec(), _validationStringLocalizer["EmpCertCertificationIdRequired"]));
            AddRule(new ValidationRule<EmployeeCertification>(new EmpCertIssueDateRequiredSpec(), _validationStringLocalizer["EmpCertIssueDateRequired"]));
        }
    }
}
