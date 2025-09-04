using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_TrainingTopicsService : Common.Service<InstructorWorkbook_TrainingTopics>, IInstructorWorkbook_TrainingTopicsService

    {
        public InstructorWorkbook_TrainingTopicsService(IInstructorWorkbook_TrainingTopicsRepository repository, IInstructorWorkbook_TrainingTopicsValidation validation)
            : base(repository, validation)
        {
        }
    }
}