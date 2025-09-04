using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.NotificationRecipietSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class NotificationRecipietValidation : Validation<NotificationRecipiet>, INotificationRecipietValidation
    {
        public NotificationRecipietValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<NotificationRecipiet>(new NotificationRecipietAttemptDateRequiredSpec(), _validationStringLocalizer["AttemptDateRequired"]));
            AddRule(new ValidationRule<NotificationRecipiet>(new NotificationRecipietNotificationIdRequiredSpec(), _validationStringLocalizer["NotificationIdRequired"]));
            AddRule(new ValidationRule<NotificationRecipiet>(new NotificationRecipietToPersonIdRequiredSpec(), _validationStringLocalizer["PersonIdRequired"]));
        }
    }
}
