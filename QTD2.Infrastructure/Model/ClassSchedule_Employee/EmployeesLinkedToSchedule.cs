using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Employee
{
    public class EmployeesLinkedToSchedule
    {
        public int ClassEmployeeId { get; set; }
        public int EmpId { get; set; }

        public string EmployeeImage { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeePosition { get; set; }

        public string EmployeeOrganization { get; set; }

        public int? Score { get; set; }

        public string Grade { get; set; }

        public string GradeNotes { get; set; }

        public string CBTStatus { get; set; }

        public string TestStatus { get; set; }

        public string PretestStatus { get; set; }

        public string RetakeStatus { get; set; }

        public List<int> PositionIds { get; set; } = new List<int>();

        public List<int> OrganizationIds { get; set; } = new List<int>();

        public EmployeesLinkedToSchedule(int classEmployeeId, string employeeName, string employeeEmail, string employeePosition, string employeeOrganization, List<int> positionIds, List<int> organizationIds, int empId, string employeeImage, int? score, string grade, string gradeNotes, string cBTStatus, string testStatus, string pretestStatus, string retakeStatus)
        {
            ClassEmployeeId = classEmployeeId;
            EmployeeName = employeeName;
            EmployeeEmail = employeeEmail;
            EmployeePosition = employeePosition;
            EmployeeOrganization = employeeOrganization;
            PositionIds = positionIds;
            OrganizationIds = organizationIds;
            EmpId = empId;
            EmployeeImage = employeeImage;
            Score = score;
            Grade = grade;
            GradeNotes = gradeNotes;
            CBTStatus = cBTStatus;
            TestStatus = testStatus;
            PretestStatus = pretestStatus;
            RetakeStatus = retakeStatus;
        }
    }
}
