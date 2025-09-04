using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class CertificationExpiringNotification : Notification
    {
        public int EmployeeCertificationId { get; set; }

        public virtual EmployeeCertification EmployeeCertification { get; set; }

        public CertificationExpiringNotification() { }

        public CertificationExpiringNotification(DateTime dueDate, int employeeCertificationId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            EmployeeCertificationId = employeeCertificationId;
        }
    }
}
