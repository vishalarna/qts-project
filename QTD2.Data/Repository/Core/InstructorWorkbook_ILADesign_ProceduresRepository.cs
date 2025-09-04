using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_ILADesign_ProceduresRepository : Common.Repository<InstructorWorkbook_ILADesign_Procedures>, IInstructorWorkbook_ILADesign_ProceduresRepository
    {
        public InstructorWorkbook_ILADesign_ProceduresRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
