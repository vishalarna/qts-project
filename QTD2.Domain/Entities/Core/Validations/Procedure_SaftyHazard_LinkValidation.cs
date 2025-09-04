using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Procedure_SaftyHazard_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Procedure_SaftyHazard_LinkValidation : Validation<Procedure_SaftyHazard_Link>, IProcedure_SaftyHazard_LinkValidation
    {
        public Procedure_SaftyHazard_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Procedure_SaftyHazard_Link>(new Proc_SH_LinkProcedureIdRequiredSpec(), _validationStringLocalizer["Proc_SH_LinkProcedureIdRequired"]));
            AddRule(new ValidationRule<Procedure_SaftyHazard_Link>(new Proc_SH_LinkSaftyHazardIdRequiredSpec(), _validationStringLocalizer["Proc_SH_LinkSaftyHazardIdRequired"]));
        }
    }
}
