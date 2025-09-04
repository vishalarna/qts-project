using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ProcedureReview_Employee : Common.Entity
    {
        public int ProcedureReviewId { get; set; }

        public bool? ProcedureReviewResponse { get; set; }

        public string Comments { get; set; }

        public int EmployeeId { get; set; }
        public DateTime? CompletedDate { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ProcedureReview ProcedureReview { get; set; }

        public string getStatus()
        {
            if (this.IsCompleted)
            {
                return "Completed";
            }
            else if (this.IsStarted && !this.IsCompleted)
            {
                return "In Progress";
            }
            else if (!this.IsStarted && !this.IsCompleted)
            {
                return "Pending";
            }
            return "Invalid";

        }
        public ProcedureReview_Employee()
        {

        }
        public ProcedureReview_Employee(ProcedureReview procedureReview, Employee employee)
        {

            ProcedureReview = procedureReview;
            Employee = employee;
            ProcedureReviewId = procedureReview.Id;
            EmployeeId = employee.Id;
        }

        public void Complete(DateTime completionDate)
        {
            IsCompleted = true;
            CompletedDate = completionDate;
        }
    }
}
