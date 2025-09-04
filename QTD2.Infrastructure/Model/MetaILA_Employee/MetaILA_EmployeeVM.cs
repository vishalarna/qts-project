using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.MetaILA_Employee
{
    public class MetaILA_EmployeeVM
    {
        public int? EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public string? Positions { get; set; }
        public string? Organizations { get; set; }
        public MetaILA_EmployeeVM() { }
        public MetaILA_EmployeeVM(int? employeeId, string? fullName, string? positions, string? organizations, string? image)
        {
            EmployeeId = employeeId;
            FullName = fullName;
            Positions = positions;
            Organizations = organizations;
            Image = image;
        }
    }
}
