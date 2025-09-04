using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnCBT_ScormRegistration_Completed : Common.IDomainEvent, INotification
    {
        public CBT_ScormRegistration CBT_ScormRegistration;

        public OnCBT_ScormRegistration_Completed(CBT_ScormRegistration cBT_ScormRegistration)
        {
            CBT_ScormRegistration = cBT_ScormRegistration;
        }
    }
}
