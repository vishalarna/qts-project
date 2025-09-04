using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DIFSurvey_Employee : Common.Entity
    {
        public int DIFSurveyId { get; set; }
        public int EmployeeId { get; set; }
        public bool? Started { get; set; }
        public bool? Complete { get; set; }
        public int StatusId { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string? Comments { get; set; }
        public virtual DIFSurvey DIFSurvey { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual DIFSurvey_Employee_Status Status { get; set; }
        public virtual ICollection<DIFSurvey_Employee_Response> Responses { get; set; } = new List<DIFSurvey_Employee_Response>();
        public void Update()
        {
            Started = true;
            StatusId = 2;
        }

        public void Completed()
        {
            Started = true;
            CompletedDate = DateTime.Now;
            Complete = true;
            StatusId = 3;
        }
        public DIFSurvey_Employee(DIFSurvey difSurvey,Employee employee,int statusId)
        {
            DIFSurveyId = difSurvey.Id;
            EmployeeId = employee.Id;
            DIFSurvey = difSurvey;
            Employee = employee;
            StatusId = statusId;
        }

        public DIFSurvey_Employee() { }
    }
}
