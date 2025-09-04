using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_TaskRepository : Common.Repository<Version_Task>, IVersion_TaskRepository
    {
        public Version_TaskRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
