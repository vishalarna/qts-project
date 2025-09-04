using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DutyAreaRepository : Common.Repository<DutyArea>, IDutyAreaRepository
    {
        public DutyAreaRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
