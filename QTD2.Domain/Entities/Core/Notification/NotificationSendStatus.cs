using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public enum NotificationSendStatus
    {
        NotSet,
        Pending,
        Sent,
        Errored,
        Rejected,
        Sending
    }
}
