using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public  class InstructorWorkbook_ILAImplementReviewersRepository : Common.Repository<InstructorWorkbook_ILAImplementReviewers>, IInstructorWorkbook_ILAImplementReviewersRepository
    {
        public InstructorWorkbook_ILAImplementReviewersRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
