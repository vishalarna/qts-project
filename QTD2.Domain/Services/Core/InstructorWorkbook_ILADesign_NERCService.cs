using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
  public  class InstructorWorkbook_ILADesign_NERCService : Common.Service<InstructorWorkbook_ILADesign_NERC>, IInstructorWorkbook_ILADesign_NERCService
    {
        public InstructorWorkbook_ILADesign_NERCService(IInstructorWorkbook_ILADesign_NERCRepository repository, IInstructorWorkbook_ILADesign_NERCValidation validation)
            : base(repository, validation)
        {
        }
    }
}
