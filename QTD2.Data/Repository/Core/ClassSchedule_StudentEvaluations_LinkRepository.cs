using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClassSchedule_StudentEvaluations_LinkRepository : Common.Repository<ClassSchedule_StudentEvaluations_Link>, IClassSchedule_StudentEvaluations_LinkRepository
    {
        public ClassSchedule_StudentEvaluations_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}

