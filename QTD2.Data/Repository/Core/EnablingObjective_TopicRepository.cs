using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EnablingObjective_TopicRepository : Common.Repository<EnablingObjective_Topic>, IEnablingObjective_TopicRepository
    {
        public EnablingObjective_TopicRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
