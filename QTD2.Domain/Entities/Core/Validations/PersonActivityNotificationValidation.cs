using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.PersonActivityNotificationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class PersonActivityNotificationValidation : Validation<PersonActivityNotification>, IPersonActivityNotificationValidation
    {
        public PersonActivityNotificationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<PersonActivityNotification>(new PersonActivityNotificationPersonIdRequiredSpec(), _validationStringLocalizer["PersonActivityNotificationPersonIdRequiredSpec"]));
            AddRule(new ValidationRule<PersonActivityNotification>(new PersonActivityNotificationActivityNotificationIdRequiredSpec(), _validationStringLocalizer["PersonActivityNotificationActivityNotificationIdRequiredSpec"]));
        }
    }
}
