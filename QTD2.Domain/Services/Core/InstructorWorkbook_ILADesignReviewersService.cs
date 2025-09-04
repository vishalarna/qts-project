using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class InstructorWorkbook_ILADesignReviewersService : Common.Service<InstructorWorkbook_ILADesignReviewers>, IInstructorWorkbook_ILADesignReviewersService
    {
        public InstructorWorkbook_ILADesignReviewersService(IInstructorWorkbook_ILADesignReviewersRepository repository, IInstructorWorkbook_ILADesignReviewersValidation validation)
            : base(repository, validation)
        {
        }
    }
}
