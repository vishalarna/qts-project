using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurvey_EmployeeVM
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpUserName { get; set; }
        public string EmpImage { get; set; }
        public int DifSurveyId { get; set; }
        public string Positions { get; set; }
        public string Organizations { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public DIFSurvey_EmployeeVM(int id, int employeeId, string empFirstName, string empLastName, string empUserName, string empimage, int difSurveyId, string positions, string organizations,DateTime? releasedDate, DateTime? completedDate, int statusId,string status)
        {
            Id = id;
            EmployeeId = employeeId;
            DifSurveyId = difSurveyId;
            Positions = positions;
            Organizations = organizations;
            EmpFirstName = empFirstName;
            EmpLastName = empLastName;
            EmpUserName = empUserName;
            EmpImage = empimage;
            ReleasedDate = releasedDate;
            CompletedDate = completedDate;
            StatusId = statusId;
            Status = status;
        }
        public DIFSurvey_EmployeeVM()
        {

        }
    }

}
