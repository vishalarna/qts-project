namespace QTD2.Infrastructure.Model.Employee_Task
{
    public class EmployeeTaskCreateOptions
    {
        public int TaskId { get; set; }

        public int? PositionId { get; set; }

        public int? EmployeeId { get; set; }
    }
}
