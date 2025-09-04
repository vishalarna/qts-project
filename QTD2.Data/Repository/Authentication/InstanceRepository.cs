using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Repository.Authentication;

namespace QTD2.Data.Repository.Authentication
{
    public class InstanceRepository : Common.Repository<Instance>, IInstanceRepository
    {
        public InstanceRepository(QTDAuthenticationContext context)
            : base(context)
        {
        }
    }
}
