using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ProviderSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ProviderValidation : Validation.Validation<Provider>, IProviderValidation
    {
        public ProviderValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Provider>(new ProviederNameRequiredSpec(), _validationStringLocalizer["ProviederNameRequiredSpec"]));
        }
    }
}
