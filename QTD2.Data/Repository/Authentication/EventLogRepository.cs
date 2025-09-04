namespace QTD2.Data.Repository.Authentication
{
    public class EventLogRepository : Common.Repository<Domain.Entities.Authentication.EventLog>, Domain.Interfaces.Repository.Authentication.IEventLogRepository
    {
        public EventLogRepository(QTDAuthenticationContext context)
            : base(context)
        {
        }
    }
}
