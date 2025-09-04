using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TimesheetSpecs
{
    public class Timesheet_EmployeeTaskIdRequiredSpec : ISpecification<Timesheet>
    {
        public bool IsSatisfiedBy(Timesheet entity, params object[] args)
        {
            return entity.EmployeeTaskId > 0;
        }
    }
}
