using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeCertification
{
    public class EMPCertificationVM
    {
        public int EmpCertificationId { get; set; }

        public int EmployeeId { get; set; }

        public int CertificationId { get; set; }

        public DateOnly IssueDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public DateOnly? RenewalDate { get; set; }

        public double? RollOverHours { get; set; }

        public string CertificationNumber { get; set; }

        public string Name { get; set; }

        public bool IsFromHistory { get; set; }

        public bool IsExpired { get; set; }
        public bool? IsNERCCertification { get; set; }
    }
}
