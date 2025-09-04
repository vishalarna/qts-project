using System;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpCourseModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string ILATitle { get; set; }
        public string Instructor { get; set; }
        public string Location { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DefaultTimeZoneId { get; set; }

        public EmpCourseModel(string firstName, string lastName, string ilaTitle, string instructor, string location, DateTime classStartDate, DateTime classEndDate, DateTime cbtAvailableDate, DateTime cbtDueDate, string defaultTimeZoneId)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            ILATitle = ilaTitle;
            Instructor = instructor;
            Location = location;
            DefaultTimeZoneId = defaultTimeZoneId;
            StartDate = classStartDate;
            EndDate = classEndDate;
            CourseStartDate = cbtAvailableDate;
            CourseEndDate = cbtDueDate;
            CourseEndDate = cbtDueDate;
        }
    }
}
