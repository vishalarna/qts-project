using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Task_Tool_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Task_Tool_LinkValidation : Validation<Version_Task_Tool_Link>, IVersion_Task_Tool_LinkValidation
    {
        public Version_Task_Tool_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Task_Tool_Link>(new VTTL_VersionTaskIdRequiredSpec(), _validationStringLocalizer["VTTL_VersionTaskIdRequired"]));
            AddRule(new ValidationRule<Version_Task_Tool_Link>(new VTTL_VersionToolIdRequiredSpec(), _validationStringLocalizer["VTTL_VersionToolIdRequired"]));
        }
    }
}
