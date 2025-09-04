using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPProcedureReviewNotification : Notification
    {
        public int ProcedureReview_EmployeeId { get; set; }

        public virtual ProcedureReview_Employee ProcedureReview_Employee { get; set; }

        public EMPProcedureReviewNotification() { }

        public EMPProcedureReviewNotification(DateTime dueDate, int procedureReview_EmployeeId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            ProcedureReview_EmployeeId = procedureReview_EmployeeId;
        }
    }
}
