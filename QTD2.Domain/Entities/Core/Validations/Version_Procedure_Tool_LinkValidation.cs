using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Procedure_Tool_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Procedure_Tool_LinkValidation : Validation<Version_Procedure_Tool_Link>, IVersion_Procedure_Tool_LinkValidation
    {
        public Version_Procedure_Tool_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Procedure_Tool_Link>(new VPTL_VersionProcedureIdRequiredSpec(), _validationStringLocalizer["VPTL_VersionProcedureIdRequired"]));
            AddRule(new ValidationRule<Version_Procedure_Tool_Link>(new VPTL_VersionToolIdRequiredSpec(), _validationStringLocalizer["VPTL_VersionToolIdRequired"]));
        }
    }
}
