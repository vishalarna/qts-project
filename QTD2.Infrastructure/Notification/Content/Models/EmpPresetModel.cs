using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpPresetModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string CourseTitle { get; set; }
        public string Instructor { get; set; }
        public string Location { get; set; }
        public DateTime ClassStartDate { get; set; }
        public DateTime ClassEndDate { get; set; }
        public string PretestTitle { get; set; }
        public int PretestId { get; set; }
        public DateTime PretestAvailableDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DefaultTimeZoneId { get; set; }
        public EmpPresetModel(string firstName,string lastName, string courseTittle,string instructor,string location,DateTime classStartDate,DateTime classEndDate,string pretestTitle,int pretestId, DateTime pretestAvailableDate, string defaultTimeZoneId)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            CourseTitle = courseTittle;
            Instructor = instructor;
            Location = location;
            ClassStartDate = classStartDate;
            ClassEndDate = classEndDate;
            PretestTitle = pretestTitle;
            PretestId = pretestId;
            PretestAvailableDate = pretestAvailableDate;
            DefaultTimeZoneId = defaultTimeZoneId;
        }
    }
}
