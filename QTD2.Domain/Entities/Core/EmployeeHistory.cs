using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EmployeeHistory : Common.Entity
    {
        public int EmployeeID { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public virtual Employee Employee { get; set; }
        public EmployeeHistoryOperationType OperationType { get; set; }
        public bool CurrentActiveStatus { get; set; }

        public EmployeeHistory()
        {
        }

        public EmployeeHistory(int employeeId, string changeNotes, DateTime changeEffectiveDate, EmployeeHistoryOperationType operationType, bool currentActiveStatus)
        {
            EmployeeID = employeeId;
            ChangeNotes = changeNotes;
            ChangeEffectiveDate = changeEffectiveDate;
            OperationType = operationType;
            CurrentActiveStatus = currentActiveStatus;
        }
    }
}
