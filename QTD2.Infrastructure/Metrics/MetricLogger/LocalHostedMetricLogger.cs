using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Metrics.MetricLogger
{
    public class LocalHostedMetricLogger : Interfaces.IMetricLogger
    {
        public Task AddLeaveInstanceEvent(string username, string instance)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginEvent(string username)
        {
            throw new NotImplementedException();
        }

        public Task AddLogoutEvent(string username)
        {
            throw new NotImplementedException();
        }

        public Task AddSelectInstanceEvent(string username, string instance)
        {
            throw new NotImplementedException();
        }
    }
}
