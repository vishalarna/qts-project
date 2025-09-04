using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Notification : Common.Entity
    {
        public int ClientSettingsNotificationStepId { get; set; }

        public DateTime DueDate { get; set; }
        public string RejectionReason { get; set; }
        public DateTime? ErrorDate { get; set; }
        public DateTime? RejectionDate { get; set; }
        public DateTime? SentDate { get; set; }
        public NotificationSendStatus Status { get; set; }
        public int? ToPersonId { get; set; }
        public string OthersEmailAddresses {  get; set; }
        public virtual ClientSettings_Notification_Step ClientSettings_Notification_Step { get; set; }
        public virtual Person ToPerson { get; set; }

        public Notification() { }

        public Notification(DateTime dueDate, int? toPersonId, int clientSettings_NotificationStepId)
        {
            ToPersonId = toPersonId;
            DueDate = dueDate;
            Status = NotificationSendStatus.Pending;
            ClientSettingsNotificationStepId = clientSettings_NotificationStepId;
        }

        public Notification(DateTime dueDate, string othersEmailAddresses, int clientSettings_NotificationStepId)
        {
            OthersEmailAddresses = othersEmailAddresses;
            DueDate = dueDate;
            Status = NotificationSendStatus.Pending;
            ClientSettingsNotificationStepId = clientSettings_NotificationStepId;
        }

        public void Send()
        {
            SentDate = DateTime.Now;
            Status = NotificationSendStatus.Sent;
        }

        public void Reject(string rejectionReason)
        {
            RejectionDate = DateTime.Now;
            Status = NotificationSendStatus.Rejected;
            RejectionReason = rejectionReason;
        }

        public void Error()
        {
            ErrorDate = DateTime.Now;
            Status = NotificationSendStatus.Errored;
        }
    }
}
