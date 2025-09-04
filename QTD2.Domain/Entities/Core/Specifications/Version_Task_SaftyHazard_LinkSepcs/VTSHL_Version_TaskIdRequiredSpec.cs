using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Task_SaftyHazard_LinkSepcs
{
    public class VTSHL_Version_TaskIdRequiredSpec : ISpecification<Version_Task_SaftyHazard_Link>
    {
        public bool IsSatisfiedBy(Version_Task_SaftyHazard_Link entity, params object[] args)
        {
            return entity.Version_TaskId > 0;
        }
    }
}
