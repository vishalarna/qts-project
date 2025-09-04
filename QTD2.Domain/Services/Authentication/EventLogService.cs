using QTD2.Domain.Entities.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QTD2.Domain.Services.Authentication
{
    public class EventLogService : Common.Service<EventLog>, Interfaces.Service.Authentication.IEventLogService
    {
        public EventLogService(
                Interfaces.Repository.Authentication.IEventLogRepository repository,
                Interfaces.Validation.Authentication.IEventLogValidation validation)
            : base(repository, validation)
        {
        }
    }
}
