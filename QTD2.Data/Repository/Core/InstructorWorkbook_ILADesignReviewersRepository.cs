using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public  class InstructorWorkbook_ILADesignReviewersRepository : Common.Repository<InstructorWorkbook_ILADesignReviewers>, IInstructorWorkbook_ILADesignReviewersRepository
    {
        public InstructorWorkbook_ILADesignReviewersRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
