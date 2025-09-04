using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Authentication;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication.Validations
{
    public class IdentityProviderValidation : Validation<IdentityProvider>, IIdentityProviderValidation
    {
        public IdentityProviderValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {

        }
    }
}
