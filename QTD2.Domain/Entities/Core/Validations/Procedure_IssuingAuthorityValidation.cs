using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Procedure_IssuingAuthoritySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Procedure_IssuingAuthorityValidation : Validation<Procedure_IssuingAuthority>, IProcedure_IssuingAuthorityValidation
    {
        public Procedure_IssuingAuthorityValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Procedure_IssuingAuthority>(new Procedure_IssuingAuthorityDescriptionRequiredSpec(), _validationStringLocalizer["Proc_IADescriptionRequired"]));
        }
    }
}
