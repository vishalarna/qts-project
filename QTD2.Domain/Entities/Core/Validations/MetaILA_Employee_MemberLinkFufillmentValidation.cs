using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class MetaILA_Employee_MemberLinkFufillmentValidation : Validation<MetaILA_Employee_MemberLinkFufillment>, IMetaILA_Employee_MemberLinkFufillmentValidation
    {
        public MetaILA_Employee_MemberLinkFufillmentValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
         }
    }
}
