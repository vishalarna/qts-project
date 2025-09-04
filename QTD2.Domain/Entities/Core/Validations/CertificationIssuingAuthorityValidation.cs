using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CertificationIssuingAuthoritySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;


namespace QTD2.Domain.Entities.Core.Validations
{
    public class CertificationIssuingAuthorityValidation : Validation<CertificationIssuingAuthority>, ICertificationIssuingAuthorityValidation
    {
        public CertificationIssuingAuthorityValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<CertificationIssuingAuthority>(new CertIA_DescriptionRequiredSpec(), _validationStringLocalizer["CertIA_DescriptionRequiredSpec"]));
            AddRule(new ValidationRule<CertificationIssuingAuthority>(new CertIA_TitleRequiredSpec(), _validationStringLocalizer["CertIA_TitleRequiredSpec"]));
        }
    }
}
