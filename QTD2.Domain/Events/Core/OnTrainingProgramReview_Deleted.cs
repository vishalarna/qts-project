using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnTrainingProgramReview_Deleted : Common.IDomainEvent, INotification
    {
        public TrainingProgramReview TrainingProgramReview { get; }
        public OnTrainingProgramReview_Deleted(TrainingProgramReview trainingProgramReview)
        {
            TrainingProgramReview = trainingProgramReview;
        }
    }
}
