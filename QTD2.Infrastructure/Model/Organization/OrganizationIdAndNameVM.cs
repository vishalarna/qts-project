using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.EmployeeOrganization;

namespace QTD2.Infrastructure.Model.Organization
{
    public class OrganizationIdAndNameVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<EmployeeOrgIdsAndEMPNameVM> EmployeeOrganizations { get; set; } = new List<EmployeeOrgIdsAndEMPNameVM>();
    }
}
