using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class Employee
    {
        public Employee()
        {
            ClassScheduleEmployees = new HashSet<ClassScheduleEmployee>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public string EmployeeNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkLocation { get; set; }
        public string Notes { get; set; }
        public string Password { get; set; }
        public bool Tqequlator { get; set; }
        public bool? Deleted { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public string Reason { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<ClassScheduleEmployee> ClassScheduleEmployees { get; set; }
    }
}
