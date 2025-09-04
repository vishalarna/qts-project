using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeCertification
{
    public class EmployeeCertificationCustomOptions
    {
        public int EmpCertificationId { get; set; }
        public int CertificationId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? RenewalDate { get; set; }

        public string CertificateName { get; set; }

        public bool? Isrenewal { get; set; }

        public int? renewaTimeFrame { get; set; }
    }
}
