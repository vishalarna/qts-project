using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeOrganization
{
    public class EmployeeOrgIdsAndEMPNameVM
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int OrganizationId { get; set; }

        public string EMPFirstName { get; set; }

        public string EMPLastName { get; set; }

        public bool IsManager { get; set; }
    }
}
