using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnCBT_ScormUpload_Connect : Common.IDomainEvent, INotification
    {
        public CBT_ScormUpload CBT_ScormUpload;

        public OnCBT_ScormUpload_Connect(CBT_ScormUpload cBT_ScormUpload)
        {
            CBT_ScormUpload = cBT_ScormUpload;
        }
    }
}
