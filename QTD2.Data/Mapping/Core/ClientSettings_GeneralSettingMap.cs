using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_GeneralSettingMap : Common.CommonMap<ClientSettings_GeneralSettings>
    {
        public override void Configure(EntityTypeBuilder<ClientSettings_GeneralSettings> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.CompanyName).IsRequired().HasMaxLength(200);
            builder.Property(o => o.CompanyLogo).IsRequired();
            builder.Property(o => o.CompanySpecificCoursePassingScore).IsRequired();
            builder.Property(o => o.ClassStartEndTimeFormat).IsRequired();
            builder.Property(o => o.DateFormat).HasMaxLength(200);
        }
    }
}
