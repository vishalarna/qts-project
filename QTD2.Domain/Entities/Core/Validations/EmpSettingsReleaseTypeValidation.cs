using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmpSettingsReleaseTypeSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmpSettingsReleaseTypeValidation : Validation<EmpSettingsReleaseType>, IEmpSettingsReleaseTypeValidation
    {
        public EmpSettingsReleaseTypeValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EmpSettingsReleaseType>(new EmpSettingsReleaseType_TypeRequiredSpec(), _validationStringLocalizer["EmpSettingsReleaseType_TypeRequiredSpec"]));
        }
    }
}
