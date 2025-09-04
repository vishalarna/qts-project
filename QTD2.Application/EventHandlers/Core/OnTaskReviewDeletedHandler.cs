using MediatR;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnTaskReviewDeletedHandler : INotificationHandler<OnTaskReview_Deleted>
    {
        private readonly QTD2.Domain.Interfaces.Service.Core.ITrainingIssueService _trainingIssueService;

        public OnTaskReviewDeletedHandler(QTD2.Domain.Interfaces.Service.Core.ITrainingIssueService trainingIssueService)
        {
            _trainingIssueService = trainingIssueService;
        }

        public async System.Threading.Tasks.Task Handle(OnTaskReview_Deleted taskReview_Deleted, CancellationToken cancellationToken)
        {
            var trainingIssue = await _trainingIssueService.GetTrainingIssueByTaskReviewIdAsync(taskReview_Deleted.TaskReview.Id);
            if (trainingIssue != null)
            {
                trainingIssue.Delete();
                await _trainingIssueService.UpdateAsync(trainingIssue);
            }
        }
    }
}