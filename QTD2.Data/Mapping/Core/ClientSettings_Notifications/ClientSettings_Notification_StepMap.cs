using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_Notification_StepMap : Common.CommonMap<ClientSettings_Notification_Step>
    {
        public override void Configure(EntityTypeBuilder<ClientSettings_Notification_Step> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.ClientSettingsNotificationId).IsRequired();
            builder.Property(o => o.Template).IsRequired();
            builder.Property(o => o.Order).IsRequired();

            builder.HasMany(r => r.Recipients).WithOne().HasForeignKey("ClientSettingsNotificationStepId");
            builder.HasMany(r => r.AvailableCustomSettings).WithOne().HasForeignKey("ClientSettingsNotificationStepId");
            builder.HasMany(r => r.CustomSettings).WithOne().HasForeignKey("ClientSettingsNotificationStepId");
        }
    }
}
