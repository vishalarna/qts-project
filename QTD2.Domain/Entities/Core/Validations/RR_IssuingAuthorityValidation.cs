using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.RR_IssuingAuthoritySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class RR_IssuingAuthorityValidation : Validation<RR_IssuingAuthority>, IRR_IssuingAuthorityValidation
    {
        public RR_IssuingAuthorityValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<RR_IssuingAuthority>(new RR_IATitleRequiredSpec(), _validationStringLocalizer["RR_IATitleRequiredSpec"]));
            //AddRule(new ValidationRule<RR_IssuingAuthority>(new RR_IADescriptionRequiredSpec(), _validationStringLocalizer["RR_IADescriptionRequiredSpec"]));
        }
    }
}
