using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication.Specifications.InstanceSpecs;
using QTD2.Domain.Interfaces.Validation.Authentication;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Authentication.Validations
{
    public class InstanceValidation : Validation<Instance>, IInstanceValidation
    {
        public InstanceValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Instance>(new ClientIdRequiredSpec(), _validationStringLocalizer["InstanceClientIdRequired"]));
            AddRule(new ValidationRule<Instance>(new NameRequiredSpec(), _validationStringLocalizer["InstanceNameRequired"]));
        }
    }
}
