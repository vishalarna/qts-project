using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILADesign_PrerequistieService : Common.Service<InstructorWorkbook_ILADesign_Prerequistie>, IInstructorWorkbook_ILADesign_PrerequistieService
    {
        public InstructorWorkbook_ILADesign_PrerequistieService(IInstructorWorkbook_ILADesign_PrerequistieRepository repository, IInstructorWorkbook_ILADesign_PrerequistieValidation validation)
            : base(repository, validation)
        {
        }
    }
}
