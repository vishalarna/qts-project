using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
  public  class InstructorWorkbook_ILADesign_TargetAudienceService : Common.Service<InstructorWorkbook_ILADesign_TargetAudience>, IInstructorWorkbook_ILADesign_TargetAudienceService
    {
        public InstructorWorkbook_ILADesign_TargetAudienceService(IInstructorWorkbook_ILADesign_TargetAudienceRepository repository, IInstructorWorkbook_ILADesign_TargetAudienceValidation validation)
            : base(repository, validation)
        {
        }
    }
}
