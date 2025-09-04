using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Procedure_SaftyHazard_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Procedure_SaftyHazard_LinkValidation : Validation<Version_Procedure_SaftyHazard_Link>, IVersion_Procedure_SaftyHazard_LinkValidation
    {
        public Version_Procedure_SaftyHazard_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Procedure_SaftyHazard_Link>(new VPSHL_Version_ProcedureIdRequiredSpec(), _validationStringLocalizer["VPSHL_Version_ProcedureIdRequired"]));
            AddRule(new ValidationRule<Version_Procedure_SaftyHazard_Link>(new VPSHL_Version_SaftyHazardIdRequiredSpec(), _validationStringLocalizer["VPSHL_Version_SaftyHazardIdRequired"]));
        }
    }
}
