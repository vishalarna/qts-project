using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_SaftyHazardSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_SaftyHazardValidation : Validation<Version_SaftyHazard>, IVersion_SaftyHazardValidation
    {
        public Version_SaftyHazardValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_SaftyHazard>(new VSH_SaftyHazardIdRequiredSpec(), _validationStringLocalizer["VSH_SaftyHazardIdRequired"]));
            AddRule(new ValidationRule<Version_SaftyHazard>(new VSH_TitleRequiredSpec(), _validationStringLocalizer["VSH_TitleRequired"]));
            AddRule(new ValidationRule<Version_SaftyHazard>(new VSH_DescriptionRequiredSpec(), _validationStringLocalizer["VSH_DescriptionRequired"]));

            // base.AddRule(new ValidationRule<Version_SaftyHazard>(new VSH_PersonalProtectiveEquipmentRequiredSpec(), _validationStringLocalizer["VSH_PersonalProtectiveEquipmentRequired"]));
            AddRule(new ValidationRule<Version_SaftyHazard>(new VSH_MinorVersionRequiredSpec(), _validationStringLocalizer["VSH_MinorVersionRequired"]));
            AddRule(new ValidationRule<Version_SaftyHazard>(new VSH_MajorVersionRequiredSpec(), _validationStringLocalizer["VSH_MajorVersionRequired"]));
        }
    }
}
