using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnTrainingIssue_DataElementUpdate : Common.IDomainEvent, INotification
    {
        public TrainingIssue_DataElement TrainingIssue_DataElement { get; }
        public OnTrainingIssue_DataElementUpdate(TrainingIssue_DataElement trainingIssue_DataElement)
        {
            TrainingIssue_DataElement = trainingIssue_DataElement;
        }
    }
}
