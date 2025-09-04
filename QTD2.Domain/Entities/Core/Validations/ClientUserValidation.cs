using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClientUserSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClientUserValidation : Validation<ClientUser>, IClientUserValidation
    {
        public ClientUserValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClientUser>(new ClientUserPersonIdRequired(), _validationStringLocalizer["ClientUserPersonIdRequired"]));
        }
    }
}
