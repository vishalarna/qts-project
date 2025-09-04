using System;
using System.Collections.Generic;


namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeListDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Image { get; set; }
        public string EmployeeNumber { get; set; }
        public IEnumerable<EmployeePositionSummaryDTO> EmployeePositions { get; set; }
        public IEnumerable<EmployeeOrganizationSummaryDTO> EmployeeOrganizations { get; set; }
        public IEnumerable<EmployeeTaskQualificationSummaryDTO> TaskQualifications { get; set; }
        public int ClassSchedule_Employee { get; set; }
        public bool Active { get; set; }
        public bool TQEqulator { get; set; }
    }

    public class EmployeePositionSummaryDTO
    {
        public int Id { get; set; }
        public string PositionTitle { get; set; }
    }
    public class EmployeeOrganizationSummaryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsManager { get; set; }
    }

    public class EmployeeTaskQualificationSummaryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EmployeeClassScheduleSummaryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
