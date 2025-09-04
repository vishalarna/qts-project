using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjectiveSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjectiveValidation : Validation<Version_EnablingObjective>, IVersion_EnablingObjectiveValidation
    {
        public Version_EnablingObjectiveValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective>(new Version_EO_EnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["Version_EO_EnablingObjectiveIdRequired"]));
            AddRule(new ValidationRule<Version_EnablingObjective>(new Version_EO_NumberRequiredSpec(), _validationStringLocalizer["Version_EO_NumberRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective>(new Version_EO_DescriptionRequiredSpec(), _validationStringLocalizer["Version_EO_DescriptionRequired"]));
            AddRule(new ValidationRule<Version_EnablingObjective>(new Version_EO_CategoryIdRequiredSpec(), _validationStringLocalizer["Version_EO_CategoryIdRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective>(new Version_EO_SubCatIdRequiredSpec(), _validationStringLocalizer["Version_EO_SubCatIdRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective>(new Version_EO_VersionNumberRequiredSpec(), _validationStringLocalizer["Version_EO_VersionNumberRequiredSpec"]));
        }
    }
}
