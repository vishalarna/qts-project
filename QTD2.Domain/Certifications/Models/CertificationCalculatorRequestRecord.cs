using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Certifications.Models
{
    public class CertificationCalculatorRequestRecord
    {
        public int? EmployeeCertificationId { get; set; }

        public int? EmployeeCertificationHistoryId { get; set; }

        public DateOnly IssueDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public DateOnly? RenewalDate { get; set; }

        public string CertificationNumber { get; set; }

        public double? RollOverHours { get; set; }

        public bool UseIssueDate { get; set; } 

        public virtual Employee Employee { get; set; }

        public virtual Certification Certification { get; set; }
        public bool IsFromEmployeeCertification { get; set; } = false;


        public CertificationCalculatorRequestRecord(EmployeeCertification employeeCertification, bool useIssueDate = false)
        {
            EmployeeCertificationId = employeeCertification.Id;
            Employee = employeeCertification.Employee;
            Certification = employeeCertification.Certification;
            IssueDate = employeeCertification.IssueDate;
            CertificationNumber = employeeCertification.CertificationNumber;
            RollOverHours = employeeCertification.RollOverHours;
            ExpirationDate = employeeCertification.ExpirationDate;
            RenewalDate = employeeCertification.RenewalDate;
            UseIssueDate = useIssueDate;
            IsFromEmployeeCertification = true;
        }

        public CertificationCalculatorRequestRecord(EmployeeCertifictaionHistory employeeCertifictaionHistory, double? rollOverHours)
        {
            EmployeeCertificationHistoryId = employeeCertifictaionHistory.Id;
            Employee = employeeCertifictaionHistory.EmployeeCertification.Employee;
            Certification = employeeCertifictaionHistory.EmployeeCertification.Certification; // Should be the NewCertificationId, based on what mapping shows
            IssueDate = employeeCertifictaionHistory.IssueDate;
            CertificationNumber = employeeCertifictaionHistory.CertificationNumber;
            RollOverHours = rollOverHours;
            ExpirationDate = employeeCertifictaionHistory.ExpirationDate;
            RenewalDate = employeeCertifictaionHistory.IssueDate;
        }

        public CertificationCalculatorRequestRecord(Employee employee, Certification certification, DateOnly issueDate, DateOnly expirationDate)
        {
            //EmployeeCertificationId = employeeCertification.Id;
            Employee = employee;
            Certification = certification;
            IssueDate = issueDate;
            //CertificationNumber = employeeCertification.CertificationNumber;
            //RollOverHours = employeeCertification.RollOverHours;
            ExpirationDate = expirationDate;
            //RenewalDate = RenewalDate;
            UseIssueDate = true;
        }
    }
}
