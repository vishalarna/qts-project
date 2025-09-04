using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClassSchedule_Evaluator_LinksRepository : Common.Repository<ClassSchedule_Evaluator_Link>, IClassSchedule_Evaluator_LinksRepository
    {
        public ClassSchedule_Evaluator_LinksRepository(QTDContext context)
            : base(context)
        {
        }
    }
}