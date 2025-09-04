using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EmpTestRepository : Common.Repository<ClassSchedule_Roster_Response_Selection>, IEmpTestRepository
    {
        public EmpTestRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}