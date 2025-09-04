using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PublicClassScheduleRequest
{
    public class ModifyPublicClassScheduleRequestModel
    {
        public int EmployeeId { get; set; }
        public int ClassScheduleEmployeeId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string? NercCertNumber { get; set; }
        public string Company { get; set; }
        public string NercCertType { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public PublicClassScheduleRequestAction RequestedAction { get; set; }
    }
}
