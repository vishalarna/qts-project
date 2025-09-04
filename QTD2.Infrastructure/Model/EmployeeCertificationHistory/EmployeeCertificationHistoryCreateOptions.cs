using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeCertificationHistory
{
    public class EmployeeCertificationHistoryCreateOptions
    {
        public int EmployeeCertificationId { get; set; }
        public DateOnly ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public DateOnly? IssueDate { get; set; }

        public DateOnly? RenewalDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public string? CertificationNumber { get; set; }
    }
}
