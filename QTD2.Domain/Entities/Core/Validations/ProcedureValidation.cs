using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ProcedureSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ProcedureValidation : Validation<Procedure>, IProcedureValidation
    {
        public ProcedureValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Procedure>(new ProcedureIssuingAuthorityIdRequiredSpec(), _validationStringLocalizer["ProcIAIdRequired"]));
            AddRule(new ValidationRule<Procedure>(new ProcedureNumberRequiredSpec(), _validationStringLocalizer["ProcNumberInvalid"]));
           // AddRule(new ValidationRule<Procedure>(new ProcedureDescriptionRequiredSpec(), _validationStringLocalizer["ProcedureDescriptionRequiredSpec"]));
            AddRule(new ValidationRule<Procedure>(new ProcedureTitleRequiredSpec(), _validationStringLocalizer["ProcTitleRequired"]));
        }
    }
}
