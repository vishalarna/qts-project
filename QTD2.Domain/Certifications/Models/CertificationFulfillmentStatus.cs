using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Certifications.Models
{
    public class CertificationFulfillmentStatus
    {
        public int? EmployeeCertificationId { get; set; } 
        public int? EmployeeCertificationHistoryId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int CertificationId { get; set; }
        public string CertificationName { get; set; }
        public string CertificationNumber { get; set; }
        public string CertificationAcronym { get; set; }
        public int? CertificationRenewalInterval { get; set; }
        public string CertificationCertifyingBody { get; set; }
        public DateOnly IssueDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public DateOnly? RenewalDate { get; set; }
        public double RequiredHours { get; set; }
        //needs to go on EmployeCertificationHistory and calculated when we create thehistory record (is that true?  are his records created when they expire?)
        public double RolloverHours { get; set; }
        public virtual List<CertificationSubRequirement> SubRequirements { get; set; } = new List<CertificationSubRequirement>();
        public virtual List<CertificationFulfillmentRecord> FulfillmentRecords { get; set; } = new List<CertificationFulfillmentRecord>();

        public CertificationFulfillmentStatus(CertificationCalculatorRequestRecord certificationCalculatorRequestRecord)
        {
            EmployeeCertificationId = certificationCalculatorRequestRecord.EmployeeCertificationId;
            EmployeeCertificationHistoryId = certificationCalculatorRequestRecord.EmployeeCertificationHistoryId;
            Employee = certificationCalculatorRequestRecord.Employee;
            EmployeeId = certificationCalculatorRequestRecord.Employee.Id;
            EmployeeFirstName = certificationCalculatorRequestRecord.Employee.Person.FirstName;
            EmployeeLastName = certificationCalculatorRequestRecord.Employee.Person.LastName;
            CertificationId = certificationCalculatorRequestRecord.Certification.Id;
            CertificationName = certificationCalculatorRequestRecord.Certification.Name;
            CertificationNumber = certificationCalculatorRequestRecord.CertificationNumber;
            CertificationAcronym = certificationCalculatorRequestRecord.Certification.CertAcronym;
            CertificationRenewalInterval = certificationCalculatorRequestRecord.Certification.RenewalInterval;
            CertificationCertifyingBody = certificationCalculatorRequestRecord.Certification.CertifyingBody.Name;
            IssueDate = certificationCalculatorRequestRecord.IssueDate;
            ExpirationDate = certificationCalculatorRequestRecord.ExpirationDate;
            RenewalDate = certificationCalculatorRequestRecord.RenewalDate;
            RequiredHours = (certificationCalculatorRequestRecord.Certification.CreditHrsReq ?? false) ? certificationCalculatorRequestRecord.Certification.CreditHrs ?? 0 : 0;
            RolloverHours = certificationCalculatorRequestRecord.RollOverHours ?? 0;

            foreach (var certSubRequirement in certificationCalculatorRequestRecord.Certification.CertificationSubRequirements)
            {
                var certificationSubRequirement = new Models.CertificationSubRequirement();
                certificationSubRequirement.CertificationSubRequirementId = certSubRequirement.Id;
                certificationSubRequirement.CertificationSubRequirementName = certSubRequirement.ReqName;
                certificationSubRequirement.RequiredHours = certSubRequirement.ReqHour;
                SubRequirements.Add(certificationSubRequirement);
            }
        }

        public CertificationFulfillmentStatus(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
