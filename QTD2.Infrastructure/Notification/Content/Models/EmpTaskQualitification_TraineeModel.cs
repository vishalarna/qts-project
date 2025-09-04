using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
 public  class EmpTaskQualitification_TraineeModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string TaskNumber { get; set; }
        public string TaskStatement { get; set; }
        public string EvaluatorName { get; set; }


        public EmpTaskQualitification_TraineeModel(string firstName, string lastName, string taskNumber, string taskStatement, string evaluators)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            TaskNumber = taskNumber;
            TaskStatement = taskStatement;
            EvaluatorName = evaluators;
        }
    }
}
