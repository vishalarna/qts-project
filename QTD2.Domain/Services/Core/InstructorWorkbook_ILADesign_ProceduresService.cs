using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILADesign_ProceduresService : Common.Service<InstructorWorkbook_ILADesign_Procedures>, IInstructorWorkbook_ILADesign_ProceduresService
    {
        public InstructorWorkbook_ILADesign_ProceduresService(IInstructorWorkbook_ILADesign_ProceduresRepository repository, IInstructorWorkbook_ILADesign_ProceduresValidation validation)
            : base(repository, validation)
        {
        }
    }
}
