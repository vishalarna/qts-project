using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpStudentEvaluationModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string ILATitle { get; set; }
        public string Instructor { get; set; }
        public string Location { get; set; }
        public DateTime CourseEndDate { get; set; }
        public DateTime EvalDueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DefaultTimeZoneId { get; set; }
        public EmpStudentEvaluationModel(
            string firstName,
            string lastName,
            string ilaTitle,
            string instructor,
            string location,
            DateTime courseEndDate,
            DateTime evalDueDate,
            string defaultTimeZoneId,
            DateTime startDate,
            DateTime endDate)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            ILATitle = ilaTitle;
            Instructor = instructor;
            Location = location;
            CourseEndDate = courseEndDate;
            EvalDueDate = evalDueDate;
            DefaultTimeZoneId = defaultTimeZoneId;
            StartDate = startDate;
            EndDate = endDate;
        }


    }


}
