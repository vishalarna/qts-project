using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILA_DevelopService : Common.Service<InstructorWorkbook_ILA_Develop>, IInstructorWorkbook_ILA_DevelopService
    {
        public InstructorWorkbook_ILA_DevelopService(IInstructorWorkbook_ILA_DevelopRepository repository, IInstructorWorkbook_ILA_DevelopValidation validation)
            : base(repository, validation)
        {
        }
    }
}
