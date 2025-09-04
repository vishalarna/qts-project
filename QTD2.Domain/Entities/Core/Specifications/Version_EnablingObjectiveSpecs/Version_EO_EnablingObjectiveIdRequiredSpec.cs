using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjectiveSpecs
{
    public class Version_EO_EnablingObjectiveIdRequiredSpec : ISpecification<Version_EnablingObjective>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective entity, params object[] args)
        {
            return entity.EnablingObjectiveId > 0;
        }
    }
}
