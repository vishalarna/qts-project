using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILADesign_DelieveryMethodsService : Common.Service<InstructorWorkbook_ILADesign_DelieveryMethods>, IInstructorWorkbook_ILADesign_DelieveryMethodsService
    {
        public InstructorWorkbook_ILADesign_DelieveryMethodsService(IInstructorWorkbook_ILADesign_DelieveryMethodsRepository repository, IInstructorWorkbook_ILADesign_DelieveryMethodsValidation validation)
            : base(repository, validation)
        {
        }
    }
}
