using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
  public  class InstructorWorkbook_ILADesign_EnablingObjectivesService : Common.Service<InstructorWorkbook_ILADesign_EnablingObjectives>, IInstructorWorkbook_ILADesign_EnablingObjectivesService
    {
        public InstructorWorkbook_ILADesign_EnablingObjectivesService(IInstructorWorkbook_ILADesign_EnablingObjectivesRepository repository, IInstructorWorkbook_ILADesign_EnablingObjectivesValidation validation)
            : base(repository, validation)
        {
        }
    }
}
