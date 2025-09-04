using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CertificationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class CertificationValidation : Validation<Certification>, ICertificationValidation
    {
        public CertificationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Certification>(new CertificationNameRequiredSpec(), _validationStringLocalizer["CertNameRequired"]));
            AddRule(new ValidationRule<Certification>(new CertifyingBodyIdRequiredSpec(), _validationStringLocalizer["CertBodyIdRequired"]));
        }
    }
}
