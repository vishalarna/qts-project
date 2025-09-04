using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_ILADesign_TasksRepository : Common.Repository<InstructorWorkbook_ILADesign_Tasks>, IInstructorWorkbook_ILADesign_TasksRepository
    {
        public InstructorWorkbook_ILADesign_TasksRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
