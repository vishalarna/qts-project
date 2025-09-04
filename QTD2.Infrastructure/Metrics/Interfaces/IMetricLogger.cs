using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Metrics.Interfaces
{
    public interface IMetricLogger
    {
        Task AddLoginEvent(string username);
        Task AddLogoutEvent(string username);
        Task AddSelectInstanceEvent(string username, string instance);
        Task AddLeaveInstanceEvent(string username, string instance);
    }
}
