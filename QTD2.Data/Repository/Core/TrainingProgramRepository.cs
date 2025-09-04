using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TrainingProgramRepository : Common.Repository<TrainingProgram>, ITrainingProgramRepository
    {
        public TrainingProgramRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
