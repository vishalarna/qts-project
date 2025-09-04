using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication.Specifications.ClientSpecs;
using QTD2.Domain.Interfaces.Validation.Authentication;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Authentication.Validations
{
    public class ClientValidation : Validation<Client>, IClientValidation
    {
        public ClientValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Client>(new NameRequiredSpec(), _validationStringLocalizer["ClientNameRequried"]));
        }
    }
}
