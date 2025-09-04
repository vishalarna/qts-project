using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SubDutyArea_HistorySpecs
{
    public class SubDutyAreaIdRequiredSpecs : ISpecification<SubDutyArea_History>
    {
        public bool IsSatisfiedBy(SubDutyArea_History entity, params object[] args)
        {
            return entity.SubDutyAreaId > 0;
        }
    }
}
