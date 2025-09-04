using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class PublicClassScheduleRequestRepository : Common.Repository<PublicClassScheduleRequest>, IPublicClassScheduleRequestRepository
    {
        public PublicClassScheduleRequestRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
