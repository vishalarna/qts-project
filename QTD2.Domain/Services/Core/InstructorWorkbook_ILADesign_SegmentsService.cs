using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILADesign_SegmentsService : Common.Service<InstructorWorkbook_ILADesign_Segments>, IInstructorWorkbook_ILADesign_SegmentsService
    {
        public InstructorWorkbook_ILADesign_SegmentsService(IInstructorWorkbook_ILADesign_SegmentsRepository repository, IInstructorWorkbook_ILADesign_SegmentsValidation validation)
            : base(repository, validation)
        {
        }
    }

}
