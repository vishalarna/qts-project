using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Task_Procedure_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Task_Procedure_LinkValidation : Validation<Version_Task_Procedure_Link>, IVersion_Task_Procedure_LinkValidation
    {
        public Version_Task_Procedure_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Task_Procedure_Link>(new VTPL_VersionProcedureIdRequiredSpec(), _validationStringLocalizer["VTPL_VersionProcedureIdRequired"]));
            AddRule(new ValidationRule<Version_Task_Procedure_Link>(new VTPL_VersionTaskIdRequiredSpec(), _validationStringLocalizer["VTPL_VersionTaskIdRequired"]));
        }
    }
}
