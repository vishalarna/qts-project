using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpTaskQualitification_EvaluatorModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string TaskNumber { get; set; }
        public string TaskStatement { get; set; }
        public string EvaluatorName { get; set; }
        public string TraineeName{get;set;}

        public EmpTaskQualitification_EvaluatorModel(string firstName,string lastName, string taskNumber, string taskStatement, string traineeName)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            TaskNumber = taskNumber;
            TaskStatement = taskStatement;
            TraineeName = traineeName;
        }

    }


}
