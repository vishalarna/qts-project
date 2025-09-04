using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Services.Core
{
    public class DIFSurvey_Task_TrainingFrequencyService : Common.Service<DIFSurvey_Task_TrainingFrequency>, IDIFSurvey_Task_TrainingFrequencyService
    {
        public DIFSurvey_Task_TrainingFrequencyService(IDIFSurvey_Task_TrainingFrequencyRepository repository, IDIFSurvey_Task_TrainingFrequencyValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<IEnumerable<DIFSurvey_Task_TrainingFrequency>> GetAllAsync()
        {
            var difSurvey_Task_TrainingFrequencyList = await AllAsync();
            return difSurvey_Task_TrainingFrequencyList;
        }

    }
}