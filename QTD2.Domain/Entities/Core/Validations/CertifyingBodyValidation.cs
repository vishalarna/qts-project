using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CertifyingBodySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class CertifyingBodyValidation : Validation<CertifyingBody>, ICertifyingBodyValidation
    {
        public CertifyingBodyValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<CertifyingBody>(new CertifyingBodyNameRequiredSpec(), _validationStringLocalizer["CertBodyNameRequired"]));
        }
    }
}
