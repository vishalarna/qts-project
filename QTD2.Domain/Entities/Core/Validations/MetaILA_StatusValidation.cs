using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.MetaILA_StatusSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class MetaILA_StatusValidation : Validation.Validation<MetaILA_Status>, IMetaILA_StatusValidation
    {
        public MetaILA_StatusValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<MetaILA_Status>(new MetaILA_StatusNameRequiredSpecs(), _validationStringLocalizer["MetaILA_StatusNameRequiredSpecs"]));
        }
    }
}
