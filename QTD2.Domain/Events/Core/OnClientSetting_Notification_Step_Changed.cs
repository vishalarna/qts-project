using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnClientSetting_Notification_Step_Changed : Common.IDomainEvent, INotification
    {
        public ClientSettings_Notification_Step ClientSettings_Notification_Step { get; }

        public OnClientSetting_Notification_Step_Changed(ClientSettings_Notification_Step clientSettingsNotificationStep)
        {
            ClientSettings_Notification_Step = clientSettingsNotificationStep;
        }
    }
}
