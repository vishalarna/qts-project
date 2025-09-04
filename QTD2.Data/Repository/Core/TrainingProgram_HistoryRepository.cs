using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TrainingProgram_HistoryRepository : Common.Repository<TrainingProgram_History>, ITrainingProgram_HistoryRepository
    {
        public TrainingProgram_HistoryRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
