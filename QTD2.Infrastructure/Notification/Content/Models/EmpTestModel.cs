using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpTestModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string CourseTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TestTitle { get; set; }
        public int TestId { get; set; }
        public DateTime ClassEndDate { get; set; }
        public DateTime TestDueDate { get; set; }
        public string Instructor { get; set; }
        public string Location { get; set; }
        public string DefaultTimeZoneId { get; set; }

        public EmpTestModel(string firstName, string lastName, string location, string instructor, string courseTittle, DateTime startDate, DateTime endDate, string testTitle, int testId, DateTime classEndDate, DateTime testDueDate,string defaultTimeZoneId)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            CourseTitle = courseTittle;
            StartDate = startDate;
            EndDate = endDate;
            TestTitle = testTitle;
            TestId = testId;
            ClassEndDate = classEndDate;
            TestDueDate = testDueDate;
            Location = location;
            Instructor = instructor;
            DefaultTimeZoneId = defaultTimeZoneId;
        }
    }


}
