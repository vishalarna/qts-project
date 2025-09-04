using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.PersonSpecs
{
    public class PersonFirstNameRequiredSpec : ISpecification<Person>
    {
        public bool IsSatisfiedBy(Person entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.FirstName);
        }
    }
}
