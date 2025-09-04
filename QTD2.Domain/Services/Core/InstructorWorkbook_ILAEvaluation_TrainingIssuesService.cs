using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILAEvaluation_TrainingIssuesService : Common.Service<InstructorWorkbook_ILAEvaluation_TrainingIssues>, IInstructorWorkbook_ILAEvaluation_TrainingIssuesService
    {
        public InstructorWorkbook_ILAEvaluation_TrainingIssuesService(IInstructorWorkbook_ILAEvaluation_TrainingIssuesRepository repository, IInstructorWorkbook_ILAEvaluation_TrainingIssuesValidation validation)
            : base(repository, validation)
        {
        }
    }
}