using System;

namespace QTD2.Infrastructure.Model.Timesheet
{
    public class TimesheetCreateOptions
    {
        public int EmployeeTaskId { get; set; }

        public DateTime Date { get; set; }

        public int MethodId { get; set; }

        public string Note { get; set; }
    }
}
