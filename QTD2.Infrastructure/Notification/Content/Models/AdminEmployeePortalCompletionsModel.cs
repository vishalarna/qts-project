using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class AdminEmployeePortalCompletionsModel
    {
        public string DefaultTimeZoneId { get; set; }
        public List<AdminEMPCompletionNotificationItem> Items { get; set; }
        public AdminEmployeePortalCompletionsModel() { }

        public AdminEmployeePortalCompletionsModel(string defaultTimeZoneId, List<AdminEMPCompletionNotificationItem> items)
        {
            DefaultTimeZoneId = defaultTimeZoneId;
            Items = items;
        }
    }
}
