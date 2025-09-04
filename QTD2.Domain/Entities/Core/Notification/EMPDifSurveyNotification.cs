using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPDifSurveyNotification : Notification
    {

        public EMPDifSurveyNotification() { }

        public EMPDifSurveyNotification(DateTime dueDate, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {

        }
    }
}
