using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications
{
    public class MetaILA_Employee_EmployeeIdRequiredSpec : ISpecification<MetaILA_Employee>
    {
        public bool IsSatisfiedBy(MetaILA_Employee entity, params object[] args)
        {
            return entity.EmployeeId > 0;
        }
    }
}
