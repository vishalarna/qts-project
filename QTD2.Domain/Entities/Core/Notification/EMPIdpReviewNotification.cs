using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPIdpReviewNotification : Notification
    {
        public int IDPId { get; set; }

        public virtual IDP IDP { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public EMPIdpReviewNotification() { }

        public EMPIdpReviewNotification(DateTime dueDate, int idpId, int employeeId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {

        }
    }
}
