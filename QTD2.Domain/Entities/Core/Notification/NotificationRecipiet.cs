using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class NotificationRecipiet : Common.Entity
    {
        public int ToPersonId { get; set; }
        public int NotificationId { get; set; }
        public DateTime AttemptDate { get; set; }
        
        public virtual Person ToPerson { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
