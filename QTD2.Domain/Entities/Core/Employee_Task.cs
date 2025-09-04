using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Employee_Task : Common.Entity
    {
        public int EmployeeId { get; set; }

        public int TaskId { get; set; }

        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }

        public bool Archived { get; protected set; }

        public virtual Employee Employee { get; set; }

        public virtual Task Task { get; set; }

        public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();

        public Employee_Task()
        {
        }

        public Employee_Task(int employeeId, int taskId)
        {
            EmployeeId = employeeId;
            TaskId = taskId;

            // MajorVersion = majorVersion;
            MajorVersion = 1;

            // MinorVersion = minorVersion;
            MinorVersion = 0;
        }

        public void Archive()
        {
            Archived = true;
        }
    }
}
