using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Task_EnablingObjective_LinkSpecs
{
    public class VTEOL_Version_EnablingObjectiveIdRequiredSpec : ISpecification<Version_Task_EnablingObjective_Link>
    {
        public bool IsSatisfiedBy(Version_Task_EnablingObjective_Link entity, params object[] args)
        {
            return entity.Version_EnablingObjectiveId > 0;
        }
    }
}
