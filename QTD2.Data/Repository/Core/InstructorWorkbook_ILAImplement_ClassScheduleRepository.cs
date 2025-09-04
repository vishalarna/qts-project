using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_ILAImplement_ClassScheduleRepository : Common.Repository<InstructorWorkbook_ILAImplement_ClassSchedule>, IInstructorWorkbook_ILAImplement_ClassSchedulerepository
    {
        public InstructorWorkbook_ILAImplement_ClassScheduleRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
