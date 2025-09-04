using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_DesignDefaultViewRepository : Common.Repository<InstructorWorkbook_DesignDefaultView>, IInstructorWorkbook_DesignDefaultViewRepository
    {
        public InstructorWorkbook_DesignDefaultViewRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
