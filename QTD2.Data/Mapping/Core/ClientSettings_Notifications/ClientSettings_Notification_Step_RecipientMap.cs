using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_Notification_Step_RecipientMap : Common.CommonMap<ClientSettings_Notification_Step_Recipient>
    {
        public override void Configure(EntityTypeBuilder<ClientSettings_Notification_Step_Recipient> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.ClientSettingsNotificationStepId).IsRequired();
            builder.Property(o => o.EmployeeId).IsRequired();
        }
    }
}
