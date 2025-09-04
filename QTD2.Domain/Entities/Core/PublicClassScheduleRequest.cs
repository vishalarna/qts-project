using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;
using QTD2.Domain.Events.Core;

namespace QTD2.Domain.Entities.Core
{
    public class PublicClassScheduleRequest : Entity
    {
        public int ClassScheduleId { get; set; }
        public string IpAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string EmailAddress { get; set; }
        public string NercCertNumber { get; set; }
        public DateTime? CertificationExpirationDate { get; set; }
        public string NercCertificationType { get; set; }
        public DateTime RequestDate { get; set; } 
        public ClassSchedule ClassSchedule { get; set; }

        public int? ClassScheduleEmployeeId { get; set; }
        public ClassSchedule_Employee ClassSchedule_Employee { get; set; }
        public PublicClassScheduleRequestStatus Status { get; set; }
        public PublicClassScheduleRequest(int classScheduleId, string ipAddress, string firstName, string lastName, string company, string emailAddress)
        {
            ClassScheduleId = classScheduleId;
            IpAddress = ipAddress;
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            EmailAddress = emailAddress;
            RequestDate = DateTime.UtcNow;
            this.Create(emailAddress);
        }

        public PublicClassScheduleRequest(int classScheduleId, string ipAddress, string firstName, string lastName, string company, string emailAddress,  string nercCertNumber, DateTime? nercCertExpiration, string nercCertType) : this(classScheduleId, ipAddress, firstName, lastName, company, emailAddress)
        {
            NercCertNumber = nercCertNumber;
            CertificationExpirationDate = nercCertExpiration;
            NercCertificationType = nercCertType;
        }
              
        public void ApprovePublicClassScheduleRequest()
        {
            Status = PublicClassScheduleRequestStatus.Accepted;
            AddDomainEvent(new OnPublicClassScheduleRequestAccepted(this));
        }

        public void DeclinePublicClassScheduleRequest()
        {
            Status = PublicClassScheduleRequestStatus.Denied;
            AddDomainEvent(new OnPublicClassScheduleRequestRejected(this));
        }

        public void RequestPublicClassSchedule()
        {
            Status = PublicClassScheduleRequestStatus.Requested;
            AddDomainEvent(new OnPublicClassScheduleRequestSubmitted(this));
        }


    }
}
