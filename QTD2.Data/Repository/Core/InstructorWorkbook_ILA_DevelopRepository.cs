using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILA_DevelopRepository : Common.Repository<InstructorWorkbook_ILA_Develop>,IInstructorWorkbook_ILA_DevelopRepository
    {
        public InstructorWorkbook_ILA_DevelopRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
