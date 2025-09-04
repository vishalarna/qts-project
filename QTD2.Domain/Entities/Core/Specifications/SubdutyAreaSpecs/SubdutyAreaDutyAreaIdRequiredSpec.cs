using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SubdutyAreaSpecs
{
    public class SubdutyAreaDutyAreaIdRequiredSpec : ISpecification<SubdutyArea>
    {
        public bool IsSatisfiedBy(SubdutyArea entity, params object[] args)
        {
            return entity.DutyAreaId > 0;
        }
    }
}
