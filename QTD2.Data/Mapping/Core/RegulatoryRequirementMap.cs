using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class RegulatoryRequirementMap : Common.CommonMap<RegulatoryRequirement>
    {
        public override void Configure(EntityTypeBuilder<RegulatoryRequirement> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Number).HasMaxLength(50).IsRequired();
            builder.Property(o => o.Title).IsRequired().HasMaxLength(200);
            builder.Property(o => o.Description);
            builder.Property(o => o.RevisionNumber);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.Uploads);
            builder.Property(o => o.HyperLink);

            builder.HasOne(o => o.RR_IssuingAuthority).WithMany(m => m.RegulatoryRequirements).HasForeignKey(k => k.IssuingAuthorityId);
        }
    }
}
