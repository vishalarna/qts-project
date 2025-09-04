using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.OrganizationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class OrganizationValidation : Validation<Organization>, IOrganizationValidation
    {
        public OrganizationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Organization>(new OrganizatonNameRequiredSpec(), _validationStringLocalizer["OrganizationNameRequired"]));
        }
    }
}
