using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.RegulatoryRequirementSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class RegulatoryRequirementValidation : Validation<RegulatoryRequirement>, IRegulatoryRequirementValidation
    {
        public RegulatoryRequirementValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<RegulatoryRequirement>(new RRTitleRequiredSpec(), _validationStringLocalizer["RRTitleRequiredSpec"]));
            AddRule(new ValidationRule<RegulatoryRequirement>(new RRNumberRequiredSpec(), _validationStringLocalizer["RRNumberRequiredSpec"]));
            AddRule(new ValidationRule<RegulatoryRequirement>(new RRIssuingAuthorityIdRequiredSpec(), _validationStringLocalizer["RRIssuingAuthorityIdRequiredSpec"]));
        }
    }
}
