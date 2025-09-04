using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeeHistorySpecs
{
    public class EmployeeHistory_EmpIDRequiredSpec : ISpecification<EmployeeHistory>
    {
        public bool IsSatisfiedBy(EmployeeHistory entity, params object[] args)
        {
            return entity.EmployeeID > 0;
        }
    }
}
