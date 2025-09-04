using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SaftyHazard_CategoryRepository : Common.Repository<SaftyHazard_Category>, ISaftyHazard_CategoryRepository
    {
        public SaftyHazard_CategoryRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
