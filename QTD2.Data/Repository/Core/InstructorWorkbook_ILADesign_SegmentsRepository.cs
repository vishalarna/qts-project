using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_ILADesign_SegmentsRepository : Common.Repository<InstructorWorkbook_ILADesign_Segments>, IInstructorWorkbook_ILADesign_SegmentsRepository
    {
        public InstructorWorkbook_ILADesign_SegmentsRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
