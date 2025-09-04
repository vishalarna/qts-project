using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EnablingObjective_SubCategoryRepository : Common.Repository<EnablingObjective_SubCategory>, IEnablingObjective_SubCategoryRepository
    {
        public EnablingObjective_SubCategoryRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
