using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TimesheetSpecs
{
    public class Timesheet_MethodIdRequiredSpec : ISpecification<Timesheet>
    {
        public bool IsSatisfiedBy(Timesheet entity, params object[] args)
        {
            return entity.MethodId > 0;
        }
    }
}
