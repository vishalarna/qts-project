using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_NotificationSpecs
{
    public class ClientSettings_Notification_Step_Recipient_EmployeeIdRequiredSpec : ISpecification<ClientSettings_Notification>
    {
        public bool IsSatisfiedBy(ClientSettings_Notification entity, params object[] args)
        {
            foreach (var step in entity.Steps)
            {
                foreach (var recipient in step.Recipients)
                {
                    if (recipient.EmployeeId <= 0) return false;
                }
            }

            return true;
        }
    }
}
