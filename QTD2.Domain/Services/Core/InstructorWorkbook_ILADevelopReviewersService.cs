using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILADevelopReviewersService : Common.Service<InstructorWorkbook_ILADevelopReviewers>, IInstructorWorkbook_ILADevelopReviewersService
    {
        public InstructorWorkbook_ILADevelopReviewersService(IInstructorWorkbook_ILADevelopReviewersRepository repository, IInstructorWorkbook_ILADevelopReviewersValidation validation)
            : base(repository, validation)
        {
        }
    }
}
