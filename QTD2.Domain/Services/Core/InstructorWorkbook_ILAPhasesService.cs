using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILAPhasesService : Common.Service<InstructorWorkbook_ILAPhases>, IInstructorWorkbook_ILAPhasesService

    {
        public InstructorWorkbook_ILAPhasesService(IInstructorWorkbook_ILAPhasesRepository repository, IInstructorWorkbook_ILAPhasesValidation validation)
            : base(repository, validation)
        {
        }
    }
}