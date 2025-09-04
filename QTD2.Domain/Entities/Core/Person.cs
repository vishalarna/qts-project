using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class Person : Common.Entity
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Image { get; set; }

        public virtual ClientUser ClientUser { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual QTDUser QTDUser { get; set; }
        public virtual ICollection<PersonActivityNotification> PersonActivityNotifications { get; set; } = new List<PersonActivityNotification>();
        public Person(string firstName, string middleName, string lastName, string username, string image)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Username = username;
            Image = image;
        }

        public Person()
        {
        }

        public void setUpdatedValues(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;

        }
        public PersonActivityNotification LinkActivityNotification(ActivityNotification activityNotification)
        {
            PersonActivityNotification personActivityNotification = PersonActivityNotifications.FirstOrDefault(x => x.ActivityNotificationId == activityNotification.Id && x.PersonId == this.Id);
            if (personActivityNotification != null)
            {
                return personActivityNotification;
            }

            personActivityNotification = new PersonActivityNotification(this, activityNotification);
            AddEntityToNavigationProperty<PersonActivityNotification>(personActivityNotification);
            return personActivityNotification;
        }

        public void UnlinkActivityNotification(ActivityNotification activityNotification)
        {

            List<PersonActivityNotification> personActivityNotifications = PersonActivityNotifications.Where(x => x.ActivityNotificationId == activityNotification.Id && x.PersonId == this.Id).ToList();
            if (personActivityNotifications.Count != 0)
            {
                foreach (var personActivity in personActivityNotifications)
                {
                    personActivity.Delete();
                }
            }
        }
    }
}
