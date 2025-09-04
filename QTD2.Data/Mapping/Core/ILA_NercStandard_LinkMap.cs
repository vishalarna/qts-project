using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_NercStandard_LinkMap : Common.CommonMap<ILA_NercStandard_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_NercStandard_Link> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.CreditHoursByStd).IsRequired().HasDefaultValue(0);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_NercStandard_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.NercStandard).WithMany(m => m.ILA_NercStandard_Links).HasForeignKey(k => k.StdId).IsRequired();
            builder.HasOne(o => o.NercStandardMember).WithMany(m => m.ILA_NercStandard_Links).HasForeignKey(k => k.NERCStdMemberId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(i => new { i.ILAId, i.StdId, i.NERCStdMemberId }).IsUnique();
        }
    }
}
