using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_ILAEvaluation_TestAnalysisRepository : Common.Repository<InstructorWorkbook_ILAEvaluation_TestAnalysis>, IInstructorWorkbook_ILAEvaluation_TestAnalysisRepository
    {
        public InstructorWorkbook_ILAEvaluation_TestAnalysisRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
