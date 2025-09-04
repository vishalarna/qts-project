namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeWithPositionVM
    {
        public Domain.Entities.Core.Employee Employee { get; set; }

        public Domain.Entities.Core.Position Position { get; set; }
    }
}
