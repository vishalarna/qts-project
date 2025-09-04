using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Authentication
{
    public class InstanceMap : Common.CommonMap<Instance>
    {
        public override void Configure(EntityTypeBuilder<Instance> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(250);
            builder.Property(o => o.IsInBeta).IsRequired().HasDefaultValue(false);
            builder.HasOne(o => o.Client).WithMany(m => m.Instances).HasForeignKey(k => k.ClientId).IsRequired();
            builder.HasOne(o => o.InstanceSetting).WithOne(o => o.Instance);
        }
    }
}
