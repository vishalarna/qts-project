using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpProcedureReviewModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string ProcedureNumber { get; set; }
        public string ProcedureTitle { get; set; }
        public DateTime ReviewStartdate { get; set; }
        public DateTime ReviewEndDate { get; set; }
        public string DefaultTimeZoneId { get; set; }

        public EmpProcedureReviewModel(string firstName, string lastName, string procedureNumber, string procedureTitle, DateTime reviewStartdate, DateTime reviewEndDate, string defaultTimeZoneId)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            ProcedureNumber = procedureNumber;
            ProcedureTitle = procedureTitle;
            ReviewStartdate = reviewStartdate;
            ReviewEndDate = reviewEndDate;
            DefaultTimeZoneId = defaultTimeZoneId;
        }
    }

}
