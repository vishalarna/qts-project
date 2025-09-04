using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;


namespace QTD2.Domain.Services.Core
{
    public class InstructorWorkbook_ILAEvaluationService : Common.Service<InstructorWorkbook_ILAEvaluation>, IInstructorWorkbook_ILAEvaluationService
    {
        public InstructorWorkbook_ILAEvaluationService(IInstructorWorkbook_ILAEvaluationRepository repository, IInstructorWorkbook_ILAEvaluationValidation validation)
            : base(repository, validation)
        {
        }
    }
}