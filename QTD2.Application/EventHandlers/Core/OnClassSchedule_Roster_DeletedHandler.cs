using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnClassSchedule_Roster_DeletedHandler : INotificationHandler<OnClassSchedule_Roster_Deleted>
    {
        private readonly IDocumentService _documentService;
        public OnClassSchedule_Roster_DeletedHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task Handle(OnClassSchedule_Roster_Deleted deletedClassScheduleRoster, CancellationToken cancellationToken)
        {
            var documents = await _documentService.GetActiveByLinkedDataAsync("ClassScheduleRosters", deletedClassScheduleRoster.ClassSchedule_Roster.Id);
            foreach (var doc in documents)
            {
                doc.Delete();
            }
            await _documentService.BulkUpdateAsync(documents);
        }
    }
}
