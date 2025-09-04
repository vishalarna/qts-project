using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication.Specifications.ClientSpecs;
using QTD2.Domain.Interfaces.Validation.Authentication;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Authentication.Validations
{
    public class EventLogValidation : Validation<EventLog>, IEventLogValidation
    {
        public EventLogValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
          
        }
    }
}
