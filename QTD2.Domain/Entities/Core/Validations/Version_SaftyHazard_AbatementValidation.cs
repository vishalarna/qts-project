using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_SaftyHazard_AbatementSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_SaftyHazard_AbatementValidation : Validation<Version_SaftyHazard_Abatement>, IVersion_SaftyHazard_AbatementValidation
    {
        public Version_SaftyHazard_AbatementValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_SaftyHazard_Abatement>(new VSHA_VersionSaftyHazardIdRequiredSpec(), _validationStringLocalizer["VSHA_VersionSaftyHazardIdRequired"]));
            AddRule(new ValidationRule<Version_SaftyHazard_Abatement>(new VSHA_DescriptionRequiredSpec(), _validationStringLocalizer["VSHA_DescriptionRequired"]));
            AddRule(new ValidationRule<Version_SaftyHazard_Abatement>(new VSHA_NumberRequiredSpec(), _validationStringLocalizer["VSHA_NumberRequired"]));
        }
    }
}
