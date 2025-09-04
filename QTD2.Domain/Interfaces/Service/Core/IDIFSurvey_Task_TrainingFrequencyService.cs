using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IDIFSurvey_Task_TrainingFrequencyService : Common.IService<DIFSurvey_Task_TrainingFrequency>
    {
        System.Threading.Tasks.Task<IEnumerable<DIFSurvey_Task_TrainingFrequency>> GetAllAsync();
    }
}
