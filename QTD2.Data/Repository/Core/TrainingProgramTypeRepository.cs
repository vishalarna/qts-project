using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TrainingProgramTypeRepository : Common.Repository<TrainingProgramType>, ITrainingProgramTypeRepository
    {
        public TrainingProgramTypeRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
