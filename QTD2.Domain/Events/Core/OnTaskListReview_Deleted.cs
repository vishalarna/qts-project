using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnTaskListReview_Deleted : Common.IDomainEvent, INotification
    {
        public TaskListReview TaskListReview { get; }

        public OnTaskListReview_Deleted(TaskListReview taskListReview)
        {
            TaskListReview = taskListReview;
        }
    }
}
