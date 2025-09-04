using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Authentication.Specifications.InstanceSettingSpecs
{
    public class DatabaseNameDifferSpec : ISpecification<InstanceSetting>
    {
        public bool IsSatisfiedBy(InstanceSetting entity, params object[] args)
        {
            return entity.DatabaseName != entity.Instance.Name;
        }
    }
}
