using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ProcedureReview_Employee
{
    public class EmployeesLinkedToProcedureReview
    {
        public int ProcedureReviewEmployeeId { get; set; }
        public int EmpId { get; set; }

        public string EmployeeImage { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeePosition { get; set; }

        public string EmployeeOrganization { get; set; }

        public string Response { get; set; }

        public string Comments { get; set; }

        public string Status { get; set; }
        public DateTime? CompletedDate { get; set; }

        public EmployeesLinkedToProcedureReview(DateTime? completedDate , int procedureReviewEmployeeId, string employeeName, string employeeEmail, string employeePosition, string employeeOrganization, int empId, string employeeImage, string response, string comments, string status)
        {
            ProcedureReviewEmployeeId = procedureReviewEmployeeId;
            EmployeeName = employeeName;
            EmployeeEmail = employeeEmail;
            EmployeePosition = employeePosition;
            EmployeeOrganization = employeeOrganization;
            EmpId = empId;
            EmployeeImage = employeeImage;
            Response = response;
            Comments = comments;
            Status = status;
            CompletedDate = completedDate;

        }
    }
}
