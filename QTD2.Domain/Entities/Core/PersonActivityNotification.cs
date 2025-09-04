using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class PersonActivityNotification : Common.Entity
    {
        public int PersonId { get; set; }

        public int ActivityNotificationId { get; set; }

        public virtual Person Person { get; set; }

        public virtual ActivityNotification ActivityNotification { get; set; }

        public PersonActivityNotification() { }

        public PersonActivityNotification(Person person, ActivityNotification activityNotification)
        {
            Person = person;
            ActivityNotification = activityNotification;
            PersonId = person.Id;
            ActivityNotificationId = activityNotification.Id;
        }
    }
}
