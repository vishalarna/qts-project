using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
  public  class InstructorWorkbook_ILAImplementReviewersService : Common.Service<InstructorWorkbook_ILAImplementReviewers>, IInstructorWorkbook_ILAImplementReviewersService

    {
        public InstructorWorkbook_ILAImplementReviewersService(IInstructorWorkbook_ILAImplementReviewersRepository repository, IInstructorWorkbook_ILAImplementReviewersValidation validation)
            : base(repository, validation)
        {
        }
    }
}