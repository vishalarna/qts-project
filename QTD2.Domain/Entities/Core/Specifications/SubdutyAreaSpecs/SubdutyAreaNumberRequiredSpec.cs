using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SubdutyAreaSpecs
{
    public class SubdutyAreaNumberRequiredSpec : ISpecification<SubdutyArea>
    {
        public bool IsSatisfiedBy(SubdutyArea entity, params object[] args)
        {
            return entity.SubNumber > 0;
        }
    }
}
