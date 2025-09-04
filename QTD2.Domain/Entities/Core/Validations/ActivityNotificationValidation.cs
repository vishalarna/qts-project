using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ActivityNotificationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActivityNotificationValidation : Validation<ActivityNotification>, IActivityNotificationValidation
    {
        public ActivityNotificationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActivityNotification>(new ActivityNotificationTitleRequiredSpec(), _validationStringLocalizer["ActivityNotificationTitleRequiredSpec"]));
        }
    }
}
