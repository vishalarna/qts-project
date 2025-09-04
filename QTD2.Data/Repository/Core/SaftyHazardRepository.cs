using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SaftyHazardRepository : Common.Repository<SaftyHazard>, ISaftyHazardRepository
    {
        public SaftyHazardRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
