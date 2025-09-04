using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_Tool_LinkSpecs
{
    public class VEOTL_VersionEnablingObjectiveIdRequiredSpec : ISpecification<Version_EnablingObjective_Tool_Link>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_Tool_Link entity, params object[] args)
        {
            return entity.Version_EnablingObjectiveId > 0;
        }
    }
}
