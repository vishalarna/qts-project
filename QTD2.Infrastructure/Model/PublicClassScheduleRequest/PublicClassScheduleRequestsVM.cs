using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PublicClassScheduleRequest
{
    public class PublicClassScheduleRequestsVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; } 
        public string Company { get; set; }
        public string NercCertNumber { get; set; }
        public string NercCertType { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public PublicClassScheduleIlaVM PublicClassScheduleIla { get; set; }
        public PublicClassScheduleVM PublicClassSchedule {  get; set; }
        public PublicClassScheduleRequestsVM()
        {
        }

        public PublicClassScheduleRequestsVM(string firstName, string lastName, string emailAddress, string company, string nercCertNumber, string nercCertType, DateTime? expirationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Company = company;
            NercCertNumber = nercCertNumber;
            NercCertType = nercCertType;
            ExpirationDate = expirationDate;
            
        }

    }
}
