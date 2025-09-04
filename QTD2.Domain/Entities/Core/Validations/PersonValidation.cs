using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.PersonSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class PersonValidation : Validation<Person>, IPersonValidation
    {
        public PersonValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Person>(new PersonFirstNameRequiredSpec(), _validationStringLocalizer["PersonFirstNameRequired"]));
            AddRule(new ValidationRule<Person>(new PersonLastNameRequiredSpec(), _validationStringLocalizer["PersonLastNameRequired"]));
            AddRule(new ValidationRule<Person>(new PersonUserNameRequiredSpec(), _validationStringLocalizer["PersonUserNameRequired"]));
        }
    }
}
