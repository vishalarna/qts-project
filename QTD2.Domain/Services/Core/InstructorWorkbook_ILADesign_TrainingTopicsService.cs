using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILADesign_TrainingTopicsService : Common.Service<InstructorWorkbook_ILADesign_TrainingTopics>, IInstructorWorkbook_ILADesign_TrainingTopicsService
    {
        public InstructorWorkbook_ILADesign_TrainingTopicsService(IInstructorWorkbook_ILADesign_TrainingTopicsRepository repository, IInstructorWorkbook_ILADesign_TrainingTopicsValidation validation)
            : base(repository, validation)
        {
        }
    }
}