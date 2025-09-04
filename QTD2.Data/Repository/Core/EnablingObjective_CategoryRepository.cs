using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EnablingObjective_CategoryRepository : Common.Repository<EnablingObjective_Category>, IEnablingObjective_CategoryRepository
    {
        public EnablingObjective_CategoryRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
