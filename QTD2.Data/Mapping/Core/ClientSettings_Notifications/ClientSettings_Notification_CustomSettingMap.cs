using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_Notification_CustomSettingMap : Common.CommonMap<ClientSettings_Notification_CustomSetting>
    {
        public override void Configure(EntityTypeBuilder<ClientSettings_Notification_CustomSetting> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Key).IsRequired().HasMaxLength(200);
            builder.Property(o => o.Value).IsRequired().HasMaxLength(200);
            builder.Property(o => o.ClientSettingsNotificationId).IsRequired();
        }
    }
}
