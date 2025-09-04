using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SaftyHazard_AbatementRepository : Common.Repository<SaftyHazard_Abatement>, ISaftyHazard_AbatementRepository
    {
        public SaftyHazard_AbatementRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
