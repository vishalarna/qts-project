using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SaftyHazard_ControlRepository : Common.Repository<SaftyHazard_Control>, ISaftyHazard_ControlRepository
    {
        public SaftyHazard_ControlRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
