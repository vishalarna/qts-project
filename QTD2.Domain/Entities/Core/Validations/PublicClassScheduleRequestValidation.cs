using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.PublicClassScheduleSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class PublicClassScheduleRequestValidation : Validation<PublicClassScheduleRequest>, IPublicClassScheduleRequestValidation
    {
        public PublicClassScheduleRequestValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            
            AddRule(new ValidationRule<PublicClassScheduleRequest>(new PuclicClassSchedule_FirstNameRequiredSpec(), _validationStringLocalizer["PuclicClassSchedule_FirstNameRequiredSpec"]));
            AddRule(new ValidationRule<PublicClassScheduleRequest>(new PuclicClassSchedule_LastNameRequiredSpec(), _validationStringLocalizer["PuclicClassSchedule_LastNameRequiredSpec"]));
            AddRule(new ValidationRule<PublicClassScheduleRequest>(new PuclicClassSchedule_EmailIDRequiredSpec(), _validationStringLocalizer["PuclicClassSchedule_EmailIDRequiredSpec"]));
            AddRule(new ValidationRule<PublicClassScheduleRequest>(new PuclicClassSchedule_IPAddressRequiredSpec(), _validationStringLocalizer["PuclicClassSchedule_IPAddressRequiredSpec"]));
        }
    }
}