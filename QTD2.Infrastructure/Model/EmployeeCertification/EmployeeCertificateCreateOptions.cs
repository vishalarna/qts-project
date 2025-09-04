using System;

namespace QTD2.Infrastructure.Model.EmployeeCertification
{
    public class EmployeeCertificateCreateOptions
    {
        public int CertificationId { get; set; }

        public int CertifyingBodyId { get; set; }

        public int EmployeeId { get; set; }

        public DateOnly IssueDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public DateOnly? RenewalDate { get; set; }

        public int[] CertificationIds { get; set; }

        public string? CertificationNumber { get; set; }
    }
}
