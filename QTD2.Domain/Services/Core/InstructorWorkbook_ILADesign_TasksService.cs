using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class InstructorWorkbook_ILADesign_TasksService : Common.Service<InstructorWorkbook_ILADesign_Tasks>, IInstructorWorkbook_ILADesign_TasksService
    {
        public InstructorWorkbook_ILADesign_TasksService(IInstructorWorkbook_ILADesign_TasksRepository repository, IInstructorWorkbook_ILADesign_TasksValidation validation)
            : base(repository, validation)
        {
        }
    }
}
