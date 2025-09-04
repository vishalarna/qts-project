using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Metrics.MetricLogger
{
    public class RemoteHostedMetricLogger : Interfaces.IMetricLogger
    {
        QTD2.Domain.Interfaces.Service.Authentication.IEventLogService _eventLogService;

        public RemoteHostedMetricLogger(QTD2.Domain.Interfaces.Service.Authentication.IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        public async Task AddLeaveInstanceEvent(string username, string instance)
        {
            var leaveInstanceEvent = new LeaveInstanceEvent(instance);

            var json = JsonSerializer.Serialize(leaveInstanceEvent);

            await _eventLogService.AddAsync(new EventLog(1, username, EventLogTypes.LeaveInstance, json));
        }

        public async Task AddLoginEvent(string username)
        {
            await _eventLogService.AddAsync(new EventLog(1, username, EventLogTypes.Login));
        }

        public async Task AddLogoutEvent(string username)
        {
            await _eventLogService.AddAsync(new EventLog(1, username, EventLogTypes.Logout));
        }

        public async Task AddSelectInstanceEvent(string username, string instance)
        {
            var selectInstanceEvent = new Domain.Entities.Authentication.SelectInstanceEvent(instance);

            var json = JsonSerializer.Serialize(selectInstanceEvent);

            await _eventLogService.AddAsync(new EventLog(1, username, EventLogTypes.SelectInstance, json));
        }
    }
}
