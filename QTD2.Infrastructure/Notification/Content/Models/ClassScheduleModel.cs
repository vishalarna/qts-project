using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
  public  class ClassScheduleModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        
        
        public ClassScheduleModel(string firstName, string lastName)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
        }
    }


}
