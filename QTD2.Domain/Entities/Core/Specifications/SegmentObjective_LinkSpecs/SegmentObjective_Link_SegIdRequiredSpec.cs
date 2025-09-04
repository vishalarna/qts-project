using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SegmentObjective_LinkSpecs
{
    public class SegmentObjective_Link_SegIdRequiredSpec : ISpecification<SegmentObjective_Link>
    {
        public bool IsSatisfiedBy(SegmentObjective_Link entity, params object[] args)
        {
            return entity.SegmentId > 0;
        }
    }
}
