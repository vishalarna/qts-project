using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_ProcedureSpecs
{
    public class VP_MinorVersionRequiredSpec : ISpecification<Version_Procedure>
    {
        public bool IsSatisfiedBy(Version_Procedure entity, params object[] args)
        {
            return entity.MinorVersion > 0;
        }
    }
}
