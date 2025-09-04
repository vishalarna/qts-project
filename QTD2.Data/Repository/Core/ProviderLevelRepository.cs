using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ProviderLevelRepository : Common.Repository<ProviderLevel>, IProviderLevelRepository
    {
        public ProviderLevelRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
