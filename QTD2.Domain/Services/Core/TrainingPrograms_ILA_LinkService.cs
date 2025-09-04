using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TrainingPrograms_ILA_LinkService : Common.Service<TrainingPrograms_ILA_Link>, ITrainingPrograms_ILA_LinkService
    {
        public TrainingPrograms_ILA_LinkService(ITrainingPrograms_ILA_LinkRepository trainingProgramlinkIlaRepository, ITrainingProgram_ILA_LinkValidation trainingProgramlinkILaValidation)
            : base(trainingProgramlinkIlaRepository, trainingProgramlinkILaValidation)
        {
        }
    }
}