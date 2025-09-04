using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILA_DesignService : Common.Service<InstructorWorkbook_ILA_Design>, IInstructorWorkbook_ILA_DesignService
    {
        public InstructorWorkbook_ILA_DesignService(IInstructorWorkbook_ILA_DesignRepository repository, IInstructorWorkbook_ILA_DesignValidation validation)
            : base(repository, validation)
        {
        }
    }
}
