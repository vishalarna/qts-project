using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class CertifyingBody : Common.Entity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Website { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public bool? IsNERC { get; set; }

        public string Notes { get; set; }
        public bool EnableCertifyingBodyLevelCEHEditing { get; set; }
        public virtual ICollection<Certification> Certifications { get; set; } = new List<Certification>();

        public virtual ICollection<CertifyingBody_History> CertifyingBody_Histories { get; set; } = new List<CertifyingBody_History>();

        public CertifyingBody(string name, string desc, string website, DateTime  effectivedate, bool? isNERC)
        {
            Name = name;
            Desc = desc;
            Website = website;
            EffectiveDate = effectivedate;
            IsNERC = isNERC;
        }

        public CertifyingBody()
        {
        }

        public EmployeeCertification Certify(Employee employee, int certificationId, DateOnly issueDate, DateOnly? expiryDate, DateOnly? renewalDate,int? rolloverhours, string certificationNumber)
        {
            EmployeeCertification employeeCertification = new EmployeeCertification(employee.Id, certificationId, issueDate, expiryDate, renewalDate, rolloverhours, certificationNumber);
            return employeeCertification;
        }
    }
}
