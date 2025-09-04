using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILADesign_EnablingObjectivesRepository : Common.Repository<InstructorWorkbook_ILADesign_EnablingObjectives>, IInstructorWorkbook_ILADesign_EnablingObjectivesRepository
    {
        public InstructorWorkbook_ILADesign_EnablingObjectivesRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
