using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EnablingObjectiveRepository : Common.Repository<EnablingObjective>, IEnablingObjectiveRepository
    {
        public EnablingObjectiveRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
