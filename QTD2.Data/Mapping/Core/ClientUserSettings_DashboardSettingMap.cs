using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientUserSettings_DashboardSettingMap : Common.CommonMap<ClientUserSettings_DashboardSetting>
    {
        public override void Configure(EntityTypeBuilder<ClientUserSettings_DashboardSetting> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Id).IsRequired();
            builder.Property(o => o.ClientUserId).IsRequired();
            builder.Property(o => o.DashboardSettingId).IsRequired();
            builder.Property(o => o.Enabled).IsRequired();
        }
    }
}
