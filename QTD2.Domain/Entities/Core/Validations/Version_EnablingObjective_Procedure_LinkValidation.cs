using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_Procedure_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_Procedure_LinkValidation : Validation<Version_EnablingObjective_Procedure_Link>, IVersion_EnablingObjective_Procedure_LinkValidation
    {
        public Version_EnablingObjective_Procedure_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_Procedure_Link>(new VEOPL_Version_EnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["VEOPL_Version_EnablingObjectiveIdRequired"]));
            AddRule(new ValidationRule<Version_EnablingObjective_Procedure_Link>(new VEOPL_Version_ProcedureIdRequiredSpec(), _validationStringLocalizer["VEOPL_Version_ProcedureIdRequired"]));
        }
    }
}
