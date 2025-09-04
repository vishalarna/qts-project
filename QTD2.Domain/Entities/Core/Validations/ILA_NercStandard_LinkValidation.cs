using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_NercStandard_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_NercStandard_LinkValidation : Validation<ILA_NercStandard_Link>, IILA_NercStandard_LinkValidation
    {
        public ILA_NercStandard_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_NercStandard_Link>(new ILA_NercStd_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_NercStd_LinkILAIdRequired"]));
            AddRule(new ValidationRule<ILA_NercStandard_Link>(new ILA_NercStd_LinkStdIdRequiredSpec(), _validationStringLocalizer["ILA_NercStd_LinkStdIdRequired"]));
            AddRule(new ValidationRule<ILA_NercStandard_Link>(new ILA_NercStd_LinkNercStMemberIdRequiredSpec(), _validationStringLocalizer["ILA_NercStd_LinkNercStMemberIdRequiredSpec"]));
        }
    }
}
