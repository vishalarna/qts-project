using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_SaftyHazard_ControlSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_SaftyHazard_ControlValidation : Validation<Version_SaftyHazard_Control>, IVersion_SaftyHazard_ControlValidation
    {
        public Version_SaftyHazard_ControlValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_SaftyHazard_Control>(new VSHC_VersionSaftyHazardIdRequiredSpec(), _validationStringLocalizer["VSHC_VersionSaftyHazardIdRequired"]));
            AddRule(new ValidationRule<Version_SaftyHazard_Control>(new VSHC_DescriptionRequiredSpec(), _validationStringLocalizer["VSHC_DescriptionRequired"]));
            AddRule(new ValidationRule<Version_SaftyHazard_Control>(new VSHC_NumberRequiredSpec(), _validationStringLocalizer["VSHC_NumberRequired"]));
        }
    }
}
