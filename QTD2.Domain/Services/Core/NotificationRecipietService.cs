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
    public class NotificationRecipietService : Common.Service<NotificationRecipiet>, INotificationRecipietService
    {
        public NotificationRecipietService(INotificationRecipietRepository repository, INotificationRecipietValidation validation)
            : base(repository, validation)
        {

        }

        public async Task<List<NotificationRecipiet>> GetForPersonAndTypeInLast24HoursAsync(int personId, int clientSettingsNotificationStepId)
        {
            return (
                await FindAsync(r => r.Notification.ClientSettingsNotificationStepId == clientSettingsNotificationStepId
                && r.ToPersonId == personId
                && r.Notification.SentDate > DateTime.Now.AddHours(-24))).ToList();

        }
    }
}
