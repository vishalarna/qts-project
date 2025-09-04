using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeePositionSpecs
{
    internal class EmpPosPositionIdRequiredSpec : ISpecification<EmployeePosition>
    {
        public bool IsSatisfiedBy(EmployeePosition entity, params object[] args)
        {
            return entity.PositionId > 0;
        }
    }
}
