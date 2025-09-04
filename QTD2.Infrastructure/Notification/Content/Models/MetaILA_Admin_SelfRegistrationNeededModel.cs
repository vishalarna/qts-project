using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class MetaILA_Admin_SelfRegistrationNeededModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string PreviousILATitle { get; set; }
        public string ILATitle { get; set; }
        public bool RegistrationsAvailable { get; set; }

        public MetaILA_Admin_SelfRegistrationNeededModel(string employeeFirstName, string employeeLastName, string previousILATitle, string iLATitle, bool registrationsAvailable)
        {
            EmployeeFirstName = employeeFirstName;
            EmployeeLastName = employeeLastName;
            PreviousILATitle = previousILATitle;
            ILATitle = iLATitle;
            RegistrationsAvailable = registrationsAvailable;
        }
    }
}
