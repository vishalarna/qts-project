using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ClientSettings_NotificationService : Common.Service<ClientSettings_Notification>, IClientSettings_NotificationService
    {
        public ClientSettings_NotificationService(IClientSettings_NotificationRepository repository, IClientSettings_NotificationValidation validation)
           : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<ClientSettings_Notification> GetClientSettingNotificationByName(string name)
        {
            var notifications = await FindWithIncludeAsync(r => r.Name == name, new string[] { "CustomSettings", "AvailableCustomSettings" });
            var notification = notifications.First();

            var stepsData = await FindWithIncludeAsync(r => r.Name == name, new string[] { "Steps", "Steps.CustomSettings", "Steps.AvailableCustomSettings" });
            var recepients = await FindWithIncludeAsync(r => r.Name == name, new string[] { "Steps", "Steps.Recipients" });

            var steps = stepsData.Where(r => r.Id == notification.Id).First().Steps.ToList();

            foreach (var step in steps)
            {
                step.Recipients = recepients.SelectMany(r => r.Steps).Where(r => r.Id == step.Id).First()?.Recipients ?? new List<ClientSettings_Notification_Step_Recipient>();
            }
            notification.Steps = steps;
            return notification;
        }

        public async System.Threading.Tasks.Task<ClientSettings_Notification> GetClientSettingNotificationByNameWithoutIncludes(string name)
        {
            var notifications = await FindAsync(r => r.Name == name);
            //var notification = notifications.First();
            //var stepsData = await FindWithIncludeAsync(r => r.Name == name, new string[] { "Steps", "Steps.Model", "Steps.CustomSettings", "Steps.AvailableCustomSettings" });
            //var recepients = await FindWithIncludeAsync(r => r.Name == name, new string[] { "Steps", "Steps.Recipients" });

            //var steps = stepsData.Where(r => r.Id == notification.Id).First().Steps.ToList();

            //foreach (var step in steps)
            //{
            //    step.Recipients = recepients.SelectMany(r => r.Steps).Where(r => r.Id == step.Id).First().Recipients;
            //}

            //notification.Steps = steps;

            return notifications.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<ClientSettings_Notification>> GetClientNotificationSettings()
        {
            //this gets broken up for optimization reasons
            var notifications = await FindWithIncludeAsync(r => true, new string[] { "CustomSettings", "AvailableCustomSettings" });
            var stepsData = await FindWithIncludeAsync(r => true, new string[] { "Steps", "Steps.CustomSettings", "Steps.AvailableCustomSettings" });
            var recepients = await FindWithIncludeAsync(r => true, new string[] { "Steps",  "Steps.Recipients" });

            foreach (var notification in notifications)
            {
                var steps = stepsData.Where(r => r.Id == notification.Id).First().Steps.ToList(); 

                foreach(var step in steps)
                {
                    step.Recipients = recepients.SelectMany(r => r.Steps).Where(r => r.Id == step.Id).First().Recipients;
                }

                notification.Steps = steps;
            }

            return notifications.ToList();
        }

        public Task<ClientSettings_Notification> GetCertificationExpirationNotification()
        {
            return GetClientSettingNotificationByName("Certification Expiration");
        }
    }
}
