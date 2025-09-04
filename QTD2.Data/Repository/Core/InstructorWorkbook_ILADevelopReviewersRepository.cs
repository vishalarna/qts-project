using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public  class InstructorWorkbook_ILADevelopReviewersRepository : Common.Repository<InstructorWorkbook_ILADevelopReviewers>, IInstructorWorkbook_ILADevelopReviewersRepository
    {
        public InstructorWorkbook_ILADevelopReviewersRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
