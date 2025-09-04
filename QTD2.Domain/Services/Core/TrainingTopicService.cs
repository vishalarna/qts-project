using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TrainingTopicService : Common.Service<TrainingTopic>, ITrainingTopicService
    {
        public TrainingTopicService(ITrainingTopicRepository repository, ITrainingTopicValidation validation)
            : base(repository, validation)
        {
        }
    }
}
