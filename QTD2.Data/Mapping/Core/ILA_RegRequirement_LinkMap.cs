using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_RegRequirement_LinkMap : Common.CommonMap<ILA_RegRequirement_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_RegRequirement_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_RegRequirement_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.RegulatoryRequirement).WithMany(m => m.ILA_RegRequirement_Links).HasForeignKey(k => k.RegulatoryRequirementId).IsRequired();
            builder.HasIndex(i => new { i.ILAId, i.RegulatoryRequirementId }).IsUnique();
        }
    }
}
