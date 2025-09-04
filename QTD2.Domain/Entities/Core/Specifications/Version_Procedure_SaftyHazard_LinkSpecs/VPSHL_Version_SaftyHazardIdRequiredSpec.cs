using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Procedure_SaftyHazard_LinkSpecs
{
    public class VPSHL_Version_SaftyHazardIdRequiredSpec : ISpecification<Version_Procedure_SaftyHazard_Link>
    {
        public bool IsSatisfiedBy(Version_Procedure_SaftyHazard_Link entity, params object[] args)
        {
            return entity.Version_SaftyHazardId > 0;
        }
    }
}
