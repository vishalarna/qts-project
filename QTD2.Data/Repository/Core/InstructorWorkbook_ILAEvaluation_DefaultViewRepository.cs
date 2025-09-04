using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILAEvaluation_DefaultViewRepository : Common.Repository<InstructorWorkbook_ILAEvaluation_DefaultView>, IInstructorWorkbook_ILAEvaluation_DefaultViewRepository
    {
        public InstructorWorkbook_ILAEvaluation_DefaultViewRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
