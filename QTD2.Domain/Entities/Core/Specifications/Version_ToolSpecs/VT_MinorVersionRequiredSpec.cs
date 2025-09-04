using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_ToolSpecs
{
    public class VT_MinorVersionRequiredSpec : ISpecification<Version_Tool>
    {
        public bool IsSatisfiedBy(Version_Tool entity, params object[] args)
        {
            return entity.MinorVersion > 0;
        }
    }
}
