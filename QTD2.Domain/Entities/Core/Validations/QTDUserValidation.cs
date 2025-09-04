using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassScheduleSpecs;
using QTD2.Domain.Entities.Core.Specifications.QTDUserSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class QTDUserValidation : Validation<QTDUser>, IQTDUserValidation
    {
        public QTDUserValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<QTDUser>(new QTDUser_PersonIdRequiredSpecs(), _validationStringLocalizer["QTDUser_PersonIdRequiredSpecs"]));
        }
    }
}
