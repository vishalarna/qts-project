using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_PhasesRepository : Common.Repository<InstructorWorkbook_Phases>, IInstructorWorkbook_PhasesRepository
    {
        public InstructorWorkbook_PhasesRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
