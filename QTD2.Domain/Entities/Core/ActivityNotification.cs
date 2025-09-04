using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActivityNotification : Common.Entity
    {
        public string Title { get; set; }

        public virtual ICollection<PersonActivityNotification> PersonActivityNotifications { get; set; } = new List<PersonActivityNotification>();

        public ActivityNotification()
        {
        }

        public ActivityNotification(string title)
        {
            Title = title;
        }
    }
}
