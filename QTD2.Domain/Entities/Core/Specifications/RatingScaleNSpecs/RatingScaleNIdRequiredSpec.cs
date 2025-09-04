using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RatingScaleNSpecs
{
    public class RatingScaleNIdRequiredSpec : ISpecification<RatingScaleN>
    {
        public bool IsSatisfiedBy(RatingScaleN entity, params object[] args)
        {
            return entity.Id > 0;
        }
    }
}
