using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
   public  class CertificationExpirationModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }

        public string CertificateName { get; set; }
        public string CertificateNumber { get; set; }
        public int DaysUntilCertificationExpiration { get; set; }
        public DateTime CertificateExpirationDate { get; set; }
        public string DefaultTimeZoneId { get; set; }
        public CertificationExpirationModel(string firstName, string lastName, string certificateName, string certificateNumber,int daysUntilCertificationExpiration,DateTime certificateExpirationDate, string defaultTimeZoneId)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            CertificateName = certificateName;
            CertificateNumber = certificateNumber;
            DaysUntilCertificationExpiration = daysUntilCertificationExpiration;
            CertificateExpirationDate = certificateExpirationDate;
            DefaultTimeZoneId = defaultTimeZoneId;
        }
    }
}
