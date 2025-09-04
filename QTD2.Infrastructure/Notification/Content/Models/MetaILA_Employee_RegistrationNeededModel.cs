using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class MetaILA_Employee_RegistrationNeededModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string PreviousILATitle { get; set; }
        public string ILATitle { get; set; }

        public MetaILA_Employee_RegistrationNeededModel(string employeeFirstName, string employeeLastName, string previousILATitle, string iLATitle)
        {
            EmployeeFirstName = employeeFirstName;
            EmployeeLastName = employeeLastName;
            PreviousILATitle = previousILATitle;
            ILATitle = iLATitle;
        }
    }
}
