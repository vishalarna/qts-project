using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DIFSurvey_Task_TrainingFrequencyRepository : Common.Repository<DIFSurvey_Task_TrainingFrequency>, IDIFSurvey_Task_TrainingFrequencyRepository
    {
        public DIFSurvey_Task_TrainingFrequencyRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
