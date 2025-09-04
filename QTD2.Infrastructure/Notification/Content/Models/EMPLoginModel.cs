using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EMPLoginModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EMPWebsite { get; set; }
        public string EmployeeUserName { get; set; }

        public EMPLoginModel(string firstName, string lastName, string empWebsite, string userName)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            EMPWebsite = empWebsite;
            EmployeeUserName = userName;
        }
    }
}
