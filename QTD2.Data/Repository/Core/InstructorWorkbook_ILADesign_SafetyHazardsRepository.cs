using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public  class InstructorWorkbook_ILADesign_SafetyHazardsRepository : Common.Repository<InstructorWorkbook_ILADesign_SafetyHazards>, IInstructorWorkbook_ILADesign_SafetyHazardsRepository
    {
        public InstructorWorkbook_ILADesign_SafetyHazardsRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
