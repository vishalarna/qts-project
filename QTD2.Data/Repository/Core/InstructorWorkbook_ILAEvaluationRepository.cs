using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILAEvaluationRepository : Common.Repository<InstructorWorkbook_ILAEvaluation>, IInstructorWorkbook_ILAEvaluationRepository
    {
        public InstructorWorkbook_ILAEvaluationRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
