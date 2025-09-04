using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Task_SaftyHazard_LinkSepcs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Task_SaftyHazard_LinkValidation : Validation<Version_Task_SaftyHazard_Link>, IVersion_Task_SaftyHazard_LinkValidation
    {
        public Version_Task_SaftyHazard_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Task_SaftyHazard_Link>(new VTSHL_Version_TaskIdRequiredSpec(), _validationStringLocalizer["VTSHL_Version_TaskIdRequired"]));
            AddRule(new ValidationRule<Version_Task_SaftyHazard_Link>(new VTSHL_Version_SaftyHazardIdRequiredSpec(), _validationStringLocalizer["VTSHL_Version_SaftyHazardIdRequired"]));
        }
    }
}
