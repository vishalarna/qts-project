using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_PhasesService : Common.Service<InstructorWorkbook_Phases>, IInstructorWorkbook_PhasesService

    {
        public InstructorWorkbook_PhasesService(IInstructorWorkbook_PhasesRepository repository, IInstructorWorkbook_PhasesValidation validation)
            : base(repository, validation)
        {
        }
    }
}