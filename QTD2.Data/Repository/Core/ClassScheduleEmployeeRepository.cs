using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClassScheduleEmployeeRepository : Common.Repository<ClassSchedule_Employee>, IClassScheduleEmployeeRepository
    {
        public ClassScheduleEmployeeRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
