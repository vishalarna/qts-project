using MediatR;
using QTD2.Domain.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnCertificationDeletedHandler : INotificationHandler<OnCertificationDeleted>
    {
        private readonly QTD2.Domain.Interfaces.Service.Core.IILACertificationLinkService _iLACertificationLinkService;

        public OnCertificationDeletedHandler(QTD2.Domain.Interfaces.Service.Core.IILACertificationLinkService iLACertificationLinkService)
        {
            _iLACertificationLinkService = iLACertificationLinkService;
        }

        public async System.Threading.Tasks.Task Handle(OnCertificationDeleted certificationDeleted, CancellationToken cancellationToken)
        {
            var iLACertificationLinks = await _iLACertificationLinkService.GetByCertificationIdAsync(certificationDeleted.DeletedCertification.Id);
            foreach (var iLACertificationLink in iLACertificationLinks)
            {
                iLACertificationLink.Delete();
                await _iLACertificationLinkService.UpdateAsync(iLACertificationLink);
            }
        }
    }
}