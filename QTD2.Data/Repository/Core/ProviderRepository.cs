using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ProviderRepository : Common.Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
