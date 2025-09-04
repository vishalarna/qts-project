using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class QTDUserRepository : Common.Repository<QTDUser>, IQTDUserRepository
    {
        public QTDUserRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
