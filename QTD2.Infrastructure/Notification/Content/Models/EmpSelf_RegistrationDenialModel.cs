using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpSelf_RegistrationDenialModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string CourseTitle { get; set; }
        public string CourseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DefaultTimeZoneId { get; set; }
        public EmpSelf_RegistrationDenialModel(string firstName,string lastName, string courseTitle, string courseNumber, DateTime startDate, DateTime endDate, string defaultTimeZoneId)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            CourseTitle = courseTitle;
            CourseNumber = courseNumber;
            StartDate = startDate;
            EndDate = endDate;
            DefaultTimeZoneId = defaultTimeZoneId;
        }
    }
}

