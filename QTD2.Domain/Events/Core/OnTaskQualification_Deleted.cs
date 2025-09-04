using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnTaskQualification_Deleted : Common.IDomainEvent, INotification
    {
        public TaskQualification TaskQualification { get; }

        public OnTaskQualification_Deleted(TaskQualification taskQualification)
        {
            TaskQualification = taskQualification;
        }
    }
}
