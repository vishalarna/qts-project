using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeeSpecs
{
    public class PersonIdRequiredSpec : ISpecification<Employee>
    {
        public bool IsSatisfiedBy(Employee entity, params object[] args)
        {
            return entity.PersonId > 0;
        }
    }
}
