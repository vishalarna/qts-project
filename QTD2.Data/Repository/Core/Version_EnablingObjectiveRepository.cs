using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_EnablingObjectiveRepository : Common.Repository<Version_EnablingObjective>, IVersion_EnablingObjectiveRepository
    {
        public Version_EnablingObjectiveRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
