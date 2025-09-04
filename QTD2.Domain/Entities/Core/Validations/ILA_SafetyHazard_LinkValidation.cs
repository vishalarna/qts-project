using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_SafetyHazard_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_SafetyHazard_LinkValidation : Validation<ILA_SafetyHazard_Link>, IILA_SafetyHazard_LinkValidation
    {
        public ILA_SafetyHazard_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_SafetyHazard_Link>(new ILA_SH_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_SH_LinkILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_SafetyHazard_Link>(new ILA_SH_LinkSHIdRequiredSpec(), _validationStringLocalizer["ILA_SH_LinkSHIdRequiredSpec"]));
        }
    }
}
