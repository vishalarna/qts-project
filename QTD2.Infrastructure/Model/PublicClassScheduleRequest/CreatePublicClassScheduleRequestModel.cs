using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PublicClassScheduleRequest
{
    public class CreatePublicClassScheduleRequestModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Company { get; set; }

        public string NercCertNumber { get; set; }
        public DateTime? NercCertExpiration { get; set; }
        public string NercCertType { get; set; }
    }
}
