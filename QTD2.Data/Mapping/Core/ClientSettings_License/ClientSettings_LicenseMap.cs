using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_LicenseMap : Common.CommonMap<Domain.Entities.Core.ClientSettings_License>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Core.ClientSettings_License> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.ActivationCode).IsRequired().HasMaxLength(200);
            builder.Property(o => o.ClientId).IsRequired();
            builder.Ignore("Expiration");
            builder.Ignore("LicenseType");
            builder.Ignore("TotalEmployeeRecordsAvailable");
            builder.Ignore("EmployeeRecordsUsed");
            builder.Ignore("Products");
        }
    }
}
