using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILADesign_PrerequistieRepository : Common.Repository<InstructorWorkbook_ILADesign_Prerequistie>, IInstructorWorkbook_ILADesign_PrerequistieRepository
    {
        public InstructorWorkbook_ILADesign_PrerequistieRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
