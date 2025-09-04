using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class EmployeeCertification : Common.Entity
    {
        public int EmployeeId { get; set; }

        public int CertificationId { get; set; }

        public DateOnly IssueDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public DateOnly? RenewalDate { get; set; }

        public double? RollOverHours { get; set; }
        public string CertificationNumber { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Certification Certification { get; set; }
        public virtual ICollection<EmployeeCertifictaionHistory> EmployeeCertificationHistorys { get; set; } = new List<EmployeeCertifictaionHistory>();

        public EmployeeCertification(int employeeId, int certificationId, DateOnly issueDate, DateOnly? expirationDate, DateOnly? renewalDate, int? rollOverHours,string certificationNumber)
        {
            EmployeeId = employeeId;
            CertificationId = certificationId;
            IssueDate = issueDate;
            SetExpirationDate(expirationDate);
            RenewalDate = renewalDate;
            RollOverHours = rollOverHours;
            CertificationNumber = certificationNumber;
        }

        public EmployeeCertification()
        {
        }

        public void Update()
        {
        }

        public void SetExpirationDate(DateOnly? expirationDate)
        {
            ExpirationDate = expirationDate;
            if (ExpirationDate.HasValue && ExpirationDate.Value < DateOnly.FromDateTime(DateTime.Today))
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }
    }
}
