using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILAEvaluation_DefaultViewService : Common.Service<InstructorWorkbook_ILAEvaluation_DefaultView>, IInstructorWorkbook_ILAEvaluation_DefaultViewService
    {
        public InstructorWorkbook_ILAEvaluation_DefaultViewService(IInstructorWorkbook_ILAEvaluation_DefaultViewRepository repository, IInstructorWorkbook_ILAEvaluation_DefaultViewValidation validation)
            : base(repository, validation)
        {
        }
    }
}