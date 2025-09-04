using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnTaskReview_Deleted : Common.IDomainEvent, INotification
    {
        public QTD2.Domain.Entities.Core.TaskReview TaskReview;
        public OnTaskReview_Deleted(QTD2.Domain.Entities.Core.TaskReview taskReview)
        {
            TaskReview = taskReview;
        }
    }
}