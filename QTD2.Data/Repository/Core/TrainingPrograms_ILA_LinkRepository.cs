using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
namespace QTD2.Data.Repository.Core
{
    public class TrainingPrograms_ILA_LinkRepository : Common.Repository<TrainingPrograms_ILA_Link>, ITrainingPrograms_ILA_LinkRepository
    {
        public TrainingPrograms_ILA_LinkRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
