using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpIdpReviewModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string IDPTitle { get; set; }
        public DateTime? ReviewStartdate { get; set; }
        public DateTime? ReviewEndDate { get; set; }
        public string DefaultTimeZoneId { get; set; }
        public EmpIdpReviewModel(string firstName, string lastName, string idpTittle, DateTime? reviewStartDate, DateTime? reviewEndDate, string defaultTimeZoneId)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            IDPTitle = idpTittle;
            ReviewStartdate = reviewStartDate;
            ReviewEndDate = reviewEndDate;
            DefaultTimeZoneId = defaultTimeZoneId;
        }

    }


}
