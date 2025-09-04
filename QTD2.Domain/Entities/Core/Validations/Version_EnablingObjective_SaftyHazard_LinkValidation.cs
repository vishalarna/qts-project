using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_SaftyHazard_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_SaftyHazard_LinkValidation : Validation<Version_EnablingObjective_SaftyHazard_Link>, IVersion_EnablingObjective_SaftyHazard_LinkValidation
    {
        public Version_EnablingObjective_SaftyHazard_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_SaftyHazard_Link>(new VEOSHL_Version_EnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["VEOSHL_Version_EnablingObjectiveIdRequired"]));
            AddRule(new ValidationRule<Version_EnablingObjective_SaftyHazard_Link>(new VEOSHL_Version_SaftyHazardIdRequiredSpec(), _validationStringLocalizer["VEOSHL_Version_SaftyHazardIdRequired"]));
        }
    }
}
