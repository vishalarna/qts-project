using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DIFSurvey_EmployeeRepository : Common.Repository<DIFSurvey_Employee>, IDIFSurvey_EmployeeRepository
    {
        public DIFSurvey_EmployeeRepository(QTDContext context)
            : base(context)
        {
        }
    }
}