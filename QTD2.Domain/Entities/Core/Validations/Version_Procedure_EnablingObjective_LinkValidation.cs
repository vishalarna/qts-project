using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Procedure_EnablingObjective_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Procedure_EnablingObjective_LinkValidation : Validation<Version_Procedure_EnablingObjective_Link>, IVersion_Procedure_EnablingObjective_LinkValidation
    {
        public Version_Procedure_EnablingObjective_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Procedure_EnablingObjective_Link>(new VPEOL_Version_ProcedureIdRequiredSpec(), _validationStringLocalizer["VPEOL_Version_ProcedureIdRequired"]));
            AddRule(new ValidationRule<Version_Procedure_EnablingObjective_Link>(new VPEOL_Version_EnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["VPEOL_Version_EnablingObjectiveIdRequired"]));
        }
    }
}
