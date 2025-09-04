using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnCBT_ScormUpload_Disconnect : Common.IDomainEvent, INotification
    {
        public CBT_ScormUpload CBT_ScormUploadDisconnect;

        public OnCBT_ScormUpload_Disconnect(CBT_ScormUpload cBT_ScormUploadDisconnect)
        {
            CBT_ScormUploadDisconnect = cBT_ScormUploadDisconnect;
        }
    }
}
