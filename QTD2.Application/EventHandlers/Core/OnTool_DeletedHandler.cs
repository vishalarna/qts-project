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
    public class OnTool_DeletedHandler : INotificationHandler<OnTool_Deleted>
    {
        private readonly IDocumentService _documentService;
        public OnTool_DeletedHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task Handle(OnTool_Deleted deletedTool, CancellationToken cancellationToken)
        {
            var documents = await _documentService.GetActiveByLinkedDataAsync("Tool", deletedTool.Tool.Id);
            foreach (var doc in documents)
            {
                doc.Delete();
            }
            await _documentService.BulkUpdateAsync(documents);
        }
    }
}
