using System;

namespace QTD2.Domain.Entities.Core
{
    public class Timesheet : Common.Entity
    {
        public int EmployeeTaskId { get; set; }

        public DateTime Date { get; set; }

        public int MethodId { get; set; }

        public string Note { get; set; }

        public virtual Employee_Task Employee_Task { get; set; }

        public Timesheet()
        {
        }

        public Timesheet(int employeeTaskId, DateTime date, int methodId, string note)
        {
            EmployeeTaskId = employeeTaskId;
            Date = date;
            MethodId = methodId;
            Note = note;
        }
    }
}
