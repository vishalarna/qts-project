using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Events.Core
{
    public class On_DIFSurvey_Employee_Response_Updated : Common.IDomainEvent, INotification
    {
        public DIFSurvey_Employee_Response DIFSurvey_Employee_Response { get; set; }
        public On_DIFSurvey_Employee_Response_Updated(DIFSurvey_Employee_Response difSurvey_Employee_Response)
        {
            DIFSurvey_Employee_Response = difSurvey_Employee_Response;
        }
    }
}
