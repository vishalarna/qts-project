using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_TaskSpecs
{
    public class VersionTask_MinorVersionRequiredSpec : ISpecification<Version_Task>
    {
        public bool IsSatisfiedBy(Version_Task entity, params object[] args)
        {
            return true;
        }
    }
}
