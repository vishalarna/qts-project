using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Import_CSV_VM
{
    public class ImportDatum_Employee_VM
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Middle { get; set; }
        public string EmpNum { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CertName { get; set; }
        public string CertNum { get; set; }
        public string IssueDate { get; set; }
        public string RecertDate { get; set; }
        public string CertExpDate { get; set; }
        public string PositionNum { get; set; }
        public string PositionStartDate { get; set; }
        public string PosAbbrev { get; set; }
        public string OrganizationName { get; set; }
        public List<ValidationError_VM> ValidationErrors { get; set; }
    }
}
