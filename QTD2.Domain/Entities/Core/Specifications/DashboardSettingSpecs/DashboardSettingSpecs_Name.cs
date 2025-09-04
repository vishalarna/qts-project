using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DashboardSettingSpecs
{
    public class DashboardSettingSpecs_NameRequiredSpec : ISpecification<DashboardSetting>
    {
        public bool IsSatisfiedBy(DashboardSetting entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}