using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class MetaILA_CourseworkCompletedModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string PreviousILATitle { get; set; }

        public MetaILA_CourseworkCompletedModel(string employeeFirstName, string employeeLastName, string previousILATitle)
        {
            EmployeeFirstName = employeeFirstName;
            EmployeeLastName = employeeLastName;
            PreviousILATitle = previousILATitle;
        }
    }
}
