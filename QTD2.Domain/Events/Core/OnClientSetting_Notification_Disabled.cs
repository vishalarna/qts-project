using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnClientSetting_Notification_Disabled : Common.IDomainEvent, INotification
    {
        public ClientSettings_Notification ClientSettings_Notification { get; }

        public OnClientSetting_Notification_Disabled(ClientSettings_Notification clientSettingsNotification)
        {
            ClientSettings_Notification = clientSettingsNotification;
        }
    }
}
