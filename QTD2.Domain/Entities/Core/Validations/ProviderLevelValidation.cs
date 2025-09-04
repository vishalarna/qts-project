using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ProviderLevelSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ProviderLevelValidation : Validation.Validation<ProviderLevel>, IProviderLevelValidation
    {
        public ProviderLevelValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ProviderLevel>(new ProviderLevelNameRequiredSpec(), _validationStringLocalizer["ProviderLevelNameRequiredSpec"]));
        }
    }
}
