using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_ILADesign_NERCRepository : Common.Repository<InstructorWorkbook_ILADesign_NERC>, IInstructorWorkbook_ILADesign_NERCRepository
    {
        public InstructorWorkbook_ILADesign_NERCRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
