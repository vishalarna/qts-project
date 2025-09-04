using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_Task_StepRepository : Common.Repository<Version_Task_Step>, IVersion_Task_StepRepository
    {
        public Version_Task_StepRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
