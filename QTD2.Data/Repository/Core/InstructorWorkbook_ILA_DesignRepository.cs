using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public class InstructorWorkbook_ILA_DesignRepository : Common.Repository<InstructorWorkbook_ILA_Design>, IInstructorWorkbook_ILA_DesignRepository
    {
        public InstructorWorkbook_ILA_DesignRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
