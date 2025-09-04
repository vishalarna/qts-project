using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_NotificationSpecs
{
    public class ClientSettings_Notification_NameRequiredSpec : ISpecification<ClientSettings_Notification>
    {
        public bool IsSatisfiedBy(ClientSettings_Notification entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
