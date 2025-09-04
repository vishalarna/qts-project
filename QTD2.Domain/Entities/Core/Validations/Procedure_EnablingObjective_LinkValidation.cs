using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Procedure_EnablingObjective_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Procedure_EnablingObjective_LinkValidation : Validation<Procedure_EnablingObjective_Link>, IProcedure_EnablingObjective_LinkValidation
    {
        public Procedure_EnablingObjective_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Procedure_EnablingObjective_Link>(new Proc_EO_LinkProcedureIdRequiredSpec(), _validationStringLocalizer["Proc_EO_LinkProcedureIdRequired"]));
            AddRule(new ValidationRule<Procedure_EnablingObjective_Link>(new Proc_EO_LinkEnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["Proc_EO_LinkEnablingObjectiveIdRequired"]));
        }
    }
}
