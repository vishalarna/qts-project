using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class PublicClassScheduleRequestRejectedModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DefaultTimeZoneId { get; set; }
        public PublicClassScheduleRequestRejectedModel(string firstName, string lastName, DateTime startDate, DateTime endDate, string defaultTimeZoneId)
        {
            FirstName = firstName;
            LastName = lastName;
            StartDate = startDate;
            EndDate = endDate;
            DefaultTimeZoneId = defaultTimeZoneId;
        }
    }
}
