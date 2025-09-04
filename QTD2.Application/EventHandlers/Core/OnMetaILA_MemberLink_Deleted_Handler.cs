using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Infrastructure.Notification.Interfaces;
using QTD2.Infrastructure.Notification.Notifications;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.HttpClients;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Persistence;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnMetaILA_MemberLink_Deleted_Handler : INotificationHandler<OnMetaILA_MemberLink_Deleted>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IMeta_ILAMembers_LinkService _meta_ILAMembers_LinkService;
        private readonly IMainUnitOfWork _mainUow;
        public OnMetaILA_MemberLink_Deleted_Handler(Domain.Interfaces.Service.Core.INotificationService notificationService,
            IMeta_ILAMembers_LinkService meta_ILAMembers_LinkService,
            IMainUnitOfWork mainUow
            )
        {
            _notificationService = notificationService;
            _meta_ILAMembers_LinkService = meta_ILAMembers_LinkService;
            _mainUow = mainUow;
            
        }

        public async System.Threading.Tasks.Task Handle(OnMetaILA_MemberLink_Deleted notification, CancellationToken cancellationToken)
        {
            var notifications = await _mainUow.Repository<MetaIlaSelfPacedReleasedNotification>().GetListAsync(i => i.NextMeta_ILAMembers_LinkId == notification.Meta_ILAMembers_Link.Id);
            if (notifications.Count != 0 && notifications != null)
            {
                foreach (var notificationData in notifications)
                {

                    notificationData.Delete();
                    await _notificationService.UpdateAsync(notificationData);

                }
            }
        }
    }
}
