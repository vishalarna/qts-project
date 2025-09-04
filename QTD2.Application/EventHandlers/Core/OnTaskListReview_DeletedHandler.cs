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
    public class OnTaskListReview_DeletedHandler : INotificationHandler<OnTaskListReview_Deleted>
    {
        private readonly IDocumentService _documentService;
        public OnTaskListReview_DeletedHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task Handle(OnTaskListReview_Deleted deletedTaskListReview, CancellationToken cancellationToken)
        {
            var documents = await _documentService.GetActiveByLinkedDataAsync("TaskListReview", deletedTaskListReview.TaskListReview.Id);
            foreach (var doc in documents)
            {
                doc.Delete();
            }
            await _documentService.BulkUpdateAsync(documents);
        }
    }
}
