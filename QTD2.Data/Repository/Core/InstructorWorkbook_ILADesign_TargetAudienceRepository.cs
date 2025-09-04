using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILADesign_TargetAudienceRepository : Common.Repository<InstructorWorkbook_ILADesign_TargetAudience>, IInstructorWorkbook_ILADesign_TargetAudienceRepository
    {
        public InstructorWorkbook_ILADesign_TargetAudienceRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
