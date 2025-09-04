using System;

namespace QTD2.Infrastructure.Model.EmployeeCertification
{
    public class EmployeeCertificateUpdateOptions
    {        
        public int EmployeeId { get; set; }

        public int CertificationId { get; set; }

        public string CertificationNumber { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public DateOnly IssueDate { get; set; }

        public DateOnly? RenewalDate { get; set; }
        public double? RolloverHours { get; set; }

        public DateOnly EffectedDate { get; set; }

        public string Reason { get; set; }

        public bool CertRequired { get; set; }

    }
}
