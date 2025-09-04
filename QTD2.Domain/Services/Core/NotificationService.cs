using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class NotificationService : Common.Service<Notification>, INotificationService
    {
        public NotificationService(INotificationRepository repository, INotificationValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<List<Notification>> GetDueNotificationsAsync()
        {
            var pendingNotifications = await FindWithIncludeAsync(r => r.Status == NotificationSendStatus.Pending && r.DueDate < System.DateTime.UtcNow,
                new[]
                {
                    "ClientSettings_Notification_Step.ClientSettings_Notification",
                    "EmployeeCertification",
                    "CBT",
                    "Employee",
                    "IDP",
                    "ProcedureReview_Employee",
                    "ClassScheduleEmployee",
                    "ClassSchedule_Evaluation_Roster",
                    "TaskQualification",
                    "TaskQualification_Evaluator_Link",
                    "Items"

                });
            return pendingNotifications.ToList();
        }

        public async Task<List<Notification>> GetPendingNotificationsByClientSettingsNotificationStepIdAsync(int clientSettingsNotificationStepId)
        {
            return (await
                 FindAsync(r => r.ClientSettings_Notification_Step.Id == clientSettingsNotificationStepId
                 && r.Status == NotificationSendStatus.Pending)).ToList();
        }


    }
}
