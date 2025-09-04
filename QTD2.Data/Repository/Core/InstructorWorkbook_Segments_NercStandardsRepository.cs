using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_Segments_NercStandardsRepository : Common.Repository<InstructorWorkbook_Segments_NercStandards>, IInstructorWorkbook_Segments_NercStandardsRepository
    {
        public InstructorWorkbook_Segments_NercStandardsRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
