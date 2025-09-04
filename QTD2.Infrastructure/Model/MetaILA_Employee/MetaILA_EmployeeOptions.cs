using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.MetaILA_Employee
{
    public class MetaILA_EmployeeOptions
    {
        public List<int> MetaILAIDs { get; set; }

        public List<int> EmployeeIDs{ get; set; }
        public string IsComingFrom { get; set; }

    }
}
