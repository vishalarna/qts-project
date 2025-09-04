using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class AdminEMPCompletionNotification : Notification
    {
        public List<AdminEMPCompletionNotificationItem> Items { get; set; }
        public AdminEMPCompletionNotification( List<AdminEMPCompletionNotificationItem> items, DateTime dueDate, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson,clientSettings_NotificationStepId)
        {
            Items = items;
        }
        public AdminEMPCompletionNotification() { }
    }

    public class AdminEMPCompletionNotificationItem : Common.Entity
    {
        public string CompletionType { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Title { get; set; }
        public string CompletionEntityName { get; set; }
        public int CompletionEntityId { get; set; }
        public AdminEMPCompletionNotificationItem(string completionType,DateTime completionDate,string title, int completionEntityId, string completionEntityName)
        {
            CompletionType = completionType;
            CompletionDate = completionDate;
            Title = title;
            CompletionEntityId = completionEntityId;
            CompletionEntityName = completionEntityName;
        }
        public AdminEMPCompletionNotificationItem()
        {
        }
    }
}
