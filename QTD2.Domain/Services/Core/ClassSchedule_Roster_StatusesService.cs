using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ClassSchedule_Roster_StatusesService : Common.Service<ClassSchedule_Roster_Statuses>, IClassSchedule_Roster_StatusesService
    {
        public ClassSchedule_Roster_StatusesService(IClassSchedule_Roster_StatusesRepository repository, IClassSchedule_Roster_StatusesValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<int> GetDashboardStatusAsync(string status)
        {
          var StatusId = (await FindAsync(x => x.Name == status)).FirstOrDefault().Id;
          return StatusId;
        }

    }
}
