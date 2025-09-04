using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeHistory
{
    public class EmployeeHistoryCreateOptions
    {
        public int EmployeeID { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ActionType { get; set; }
        public bool CurrentActiveStatus { get; set; }
        public EmployeeHistoryOperationType OperationType { get; set; }

        public EmployeeHistoryCreateOptions() { }

        public EmployeeHistoryCreateOptions(int employeeID, string changeNotes, DateTime changeEffectiveDate, bool currentActiveStatus, EmployeeHistoryOperationType operationType)
        {
            EmployeeID = employeeID;
            ChangeNotes = changeNotes;
            ChangeEffectiveDate = changeEffectiveDate;
            CurrentActiveStatus = currentActiveStatus;
            OperationType = operationType;
        }
    }
}
