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
using QTD2.Domain.Services.Core;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnOrganizationDeleted_Handler : INotificationHandler<OnOrganizationDeleted>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IEmployeeOrganizationService _employeeOrganizationService;

        public OnOrganizationDeleted_Handler(Domain.Interfaces.Service.Core.INotificationService notificationService,
           IEmployeeOrganizationService employeeOrganizationService
            )
        {
            _notificationService = notificationService;
            _employeeOrganizationService = employeeOrganizationService;
        }

        public async System.Threading.Tasks.Task Handle(OnOrganizationDeleted notification, CancellationToken cancellationToken)
        {
            var employeeOrgLinks = await _employeeOrganizationService.GetEmployeeOrganizationsByOrganizationIdAsync(notification.Organization.Id);

            foreach (var link in employeeOrgLinks)
            {
                link.Delete();
            }

            await _employeeOrganizationService.BulkUpdateAsync(employeeOrgLinks);
        }

    }
}
