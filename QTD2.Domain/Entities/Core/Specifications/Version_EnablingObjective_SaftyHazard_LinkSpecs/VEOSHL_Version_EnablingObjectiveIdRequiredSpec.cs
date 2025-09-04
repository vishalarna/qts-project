using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_SaftyHazard_LinkSpecs
{
    public class VEOSHL_Version_EnablingObjectiveIdRequiredSpec : ISpecification<Version_EnablingObjective_SaftyHazard_Link>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_SaftyHazard_Link entity, params object[] args)
        {
            return entity.Version_EnablingObjectiveId > 0;
        }
    }
}
