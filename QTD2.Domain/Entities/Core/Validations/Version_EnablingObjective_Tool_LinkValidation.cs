using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_Tool_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_Tool_LinkValidation : Validation<Version_EnablingObjective_Tool_Link>, IVersion_EnablingObjective_Tool_LinkValidation
    {
        public Version_EnablingObjective_Tool_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_Tool_Link>(new VEOTL_VersionEnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["VEOTL_VersionEnablingObjectiveIdRequired"]));
            AddRule(new ValidationRule<Version_EnablingObjective_Tool_Link>(new VEOTL_VersionToolIdRequiredSpec(), _validationStringLocalizer["VEOTL_VersionEnablingObjectiveIdRequired"]));
        }
    }
}
