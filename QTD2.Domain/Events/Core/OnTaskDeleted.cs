using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnTaskDeleted : Common.IDomainEvent, INotification
    {
        public QTD2.Domain.Entities.Core.Task TaskDeleted;

        public OnTaskDeleted(QTD2.Domain.Entities.Core.Task taskDeleted)
        {
            TaskDeleted = taskDeleted;
        }
    }
}