using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILA_ImplementRepository : Common.Repository<InstructorWorkbook_ILA_Implement>,IInstructorWorkbook_ILA_ImplementRepository
    {
        public InstructorWorkbook_ILA_ImplementRepository(QTDContext context)
            : base(context)
        {
        }
    }
}