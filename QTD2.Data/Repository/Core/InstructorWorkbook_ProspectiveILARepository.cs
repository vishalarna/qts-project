using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ProspectiveILARepository : Common.Repository<InstructorWorkbook_ProspectiveILA>, IInstructorWorkbook_ProspectiveILARepository
    {
        public InstructorWorkbook_ProspectiveILARepository(QTDContext context)
            : base(context)
        {

        }
    }
}
