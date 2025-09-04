using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILA_ImplementService : Common.Service<InstructorWorkbook_ILA_Implement>, IInstructorWorkbook_ILA_ImplementService
    {
        public InstructorWorkbook_ILA_ImplementService(IInstructorWorkbook_ILA_ImplementRepository repository, IInstructorWorkbook_ILA_ImplementValidation validation)
            : base(repository, validation)
        {
        }
    }
}
