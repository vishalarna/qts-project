using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
  public  class ReportService : Common.Service<Report>, IReportService
    {
        public ReportService(IReportRepository repository, IReportValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<Report>> GetAllActiveAsync()
        {
            return (await FindAsync(r => r.Active)).ToList();
        }
    }
}
