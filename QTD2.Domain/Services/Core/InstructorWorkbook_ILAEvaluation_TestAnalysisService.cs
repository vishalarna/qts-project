using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class InstructorWorkbook_ILAEvaluation_TestAnalysisService : Common.Service<InstructorWorkbook_ILAEvaluation_TestAnalysis>, IInstructorWorkbook_ILAEvaluation_TestAnalysisService
    {
        public InstructorWorkbook_ILAEvaluation_TestAnalysisService(IInstructorWorkbook_ILAEvaluation_TestAnalysisRepository repository, IInstructorWorkbook_ILAEvaluation_TestAnalysisValidation validation)
            : base(repository, validation)
        {
        }
    }
}