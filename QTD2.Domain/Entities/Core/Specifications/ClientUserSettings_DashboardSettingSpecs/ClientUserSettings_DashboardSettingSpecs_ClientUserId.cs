using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientUserSettings_DashboardSettingSpecs
{
    public class ClientUserSettings_DashboardSettingSpecs_ClientUserIdRequiredSpec : ISpecification<ClientUserSettings_DashboardSetting>
    {
        public bool IsSatisfiedBy(ClientUserSettings_DashboardSetting entity, params object[] args)
        {
            return entity.ClientUserId > 0;
        }
    }
}
