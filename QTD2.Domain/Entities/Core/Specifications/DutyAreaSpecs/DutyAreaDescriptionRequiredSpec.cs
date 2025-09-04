using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DutyAreaSpecs
{
    public class DutyAreaDescriptionRequiredSpec : ISpecification<DutyArea>
    {
        public bool IsSatisfiedBy(DutyArea entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
