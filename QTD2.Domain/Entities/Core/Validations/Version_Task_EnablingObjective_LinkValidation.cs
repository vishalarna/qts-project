using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Task_EnablingObjective_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Task_EnablingObjective_LinkValidation : Validation<Version_Task_EnablingObjective_Link>, IVersion_Task_EnablingObjective_LinkValidation
    {
        public Version_Task_EnablingObjective_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Task_EnablingObjective_Link>(new VTEOL_Version_TaskIdRequiredSpec(), _validationStringLocalizer["VTEOL_Version_TaskIdRequired"]));
            AddRule(new ValidationRule<Version_Task_EnablingObjective_Link>(new VTEOL_Version_EnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["VTEOL_Version_EnablingObjectiveIdRequired"]));
        }
    }
}
