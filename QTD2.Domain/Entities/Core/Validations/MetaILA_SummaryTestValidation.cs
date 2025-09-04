using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class MetaILA_SummaryTestValidation : Validation<MetaILA_SummaryTest>, IMetaILA_SummaryTestValidation
    {
        public MetaILA_SummaryTestValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<MetaILA_SummaryTest>(new MetaILA_SummaryTestTestIdRequiredSpec(), _validationStringLocalizer["MetaILA_SummaryTestTestIdRequiredSpec"]));
            AddRule(new ValidationRule<MetaILA_SummaryTest>(new MetaILA_SummaryTestTestTypeIdRequiredSpec(), _validationStringLocalizer["MetaILA_SummaryTestTestTypeIdRequiredSpec"]));
        }
    }
}
