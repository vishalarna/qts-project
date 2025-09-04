using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILAPhasesRepository : Common.Repository<InstructorWorkbook_ILAPhases>, IInstructorWorkbook_ILAPhasesRepository
    {
        public InstructorWorkbook_ILAPhasesRepository(QTDContext context)
            : base(context)
        {

        }
    }
}