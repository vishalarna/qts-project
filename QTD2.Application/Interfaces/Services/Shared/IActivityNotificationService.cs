using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ActivityNotification;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IActivityNotificationService
    {
        public Task<List<ActivityNotification>> GetAsync();

        public Task<ActivityNotification> GetAsync(int id);

        public Task<ActivityNotification> CreateAsync(ActivityNotificationsCreateOptions options);

        public Task<ActivityNotification> UpdateAsync(int id, ActivityNotificationsUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
