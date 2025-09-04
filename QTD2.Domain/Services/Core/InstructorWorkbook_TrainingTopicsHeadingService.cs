using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_TrainingTopicsHeadingService : Common.Service<InstructorWorkbook_TrainingTopicsHeading>, IInstructorWorkbook_TrainingTopicsHeadingService

    {
        public InstructorWorkbook_TrainingTopicsHeadingService(IInstructorWorkbook_TrainingTopicsHeadingRepository repository, IInstructorWorkbook_TrainingTopicsHeadingValidation validation)
            : base(repository, validation)
        {
        }
    }
}