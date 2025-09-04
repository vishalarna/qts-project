using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILAImplement_ClassScheduleService : Common.Service<InstructorWorkbook_ILAImplement_ClassSchedule>, IInstructorWorkbook_ILAImplement_ClassScheduleService
    {
        public InstructorWorkbook_ILAImplement_ClassScheduleService(IInstructorWorkbook_ILAImplement_ClassSchedulerepository repository, IInstructorWorkbook_ILAImplement_ClassScheduleValidation validation)
            : base(repository, validation)
        {
        }
    }
}