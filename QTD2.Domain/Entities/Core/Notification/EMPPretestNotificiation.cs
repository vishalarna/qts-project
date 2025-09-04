using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPPretestNotificiation : Notification
    {
        public int ClassScheduleRosterId { get; set; }

        public virtual ClassSchedule_Roster ClassScheduleRoster { get; set; }

        public EMPPretestNotificiation() { }

        public EMPPretestNotificiation(DateTime dueDate, int classScheduleRosterId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            ClassScheduleRosterId = classScheduleRosterId;
        }
    }
}
