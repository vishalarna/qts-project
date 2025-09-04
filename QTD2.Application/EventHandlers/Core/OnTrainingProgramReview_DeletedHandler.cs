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
    public class OnTrainingProgramReview_DeletedHandler : INotificationHandler<OnTrainingProgramReview_Deleted>
    {
        private readonly IDocumentService _documentService;
        public OnTrainingProgramReview_DeletedHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        public async System.Threading.Tasks.Task Handle(OnTrainingProgramReview_Deleted deletedTrainingProgramReview, CancellationToken cancellationToken)
        {
            var documents = await _documentService.GetActiveByLinkedDataAsync("TrainingProgramReviews", deletedTrainingProgramReview.TrainingProgramReview.Id);
            foreach (var doc in documents)
            {
                doc.Delete();
            }
            await _documentService.BulkUpdateAsync(documents);
        }
    }
}
