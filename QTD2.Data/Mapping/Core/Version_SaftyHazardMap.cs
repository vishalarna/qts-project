using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_SaftyHazardMap : Common.CommonMap<Version_SaftyHazard>
    {
        public override void Configure(EntityTypeBuilder<Version_SaftyHazard> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.PersonalProtectiveEquipment);
            builder.Property(o => o.MinorVersion).IsRequired();
            builder.Property(o => o.MajorVersion).IsRequired();
            builder.Property(o => o.VersionNumber).HasMaxLength(20);
            builder.HasOne(o => o.SaftyHazard).WithMany(m => m.Version_SaftyHazards).HasForeignKey(k => k.SaftyHazardId).IsRequired();
        }
    }
}
