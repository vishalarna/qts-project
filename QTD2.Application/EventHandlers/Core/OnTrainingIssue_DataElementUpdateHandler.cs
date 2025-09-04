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
    public class OnTrainingIssue_DataElementUpdateHandler : INotificationHandler<OnTrainingIssue_DataElementUpdate>
    {
        private readonly ITrainingProgramReview_TrainingIssue_LinkService _trainingProgramReview_TrainingIssue_LinkService;
        public OnTrainingIssue_DataElementUpdateHandler(ITrainingProgramReview_TrainingIssue_LinkService trainingProgramReview_TrainingIssue_LinkService)
        {
            _trainingProgramReview_TrainingIssue_LinkService = trainingProgramReview_TrainingIssue_LinkService;
        }
        public async Task Handle(OnTrainingIssue_DataElementUpdate notification, CancellationToken cancellationToken)
        {
            if(notification.TrainingIssue_DataElement.DataElementDisplay=="TrainingProgram")
            {
                var links = (await _trainingProgramReview_TrainingIssue_LinkService.FindAsync(x => x.TrainingIssueId == notification.TrainingIssue_DataElement.TrainingIssueId)).ToList();
                links.ForEach(x => x.Delete());
                await _trainingProgramReview_TrainingIssue_LinkService.BulkUpdateAsync(links);
            }
        }
    }
}
