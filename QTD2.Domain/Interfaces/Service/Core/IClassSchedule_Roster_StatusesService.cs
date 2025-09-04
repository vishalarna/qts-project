using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_Roster_StatusesService : Common.IService<ClassSchedule_Roster_Statuses>
    {
        public Task<int> GetDashboardStatusAsync(string status);
    }
}
