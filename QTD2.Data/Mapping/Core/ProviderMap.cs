using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ProviderMap : Common.CommonMap<Provider>
    {
        public override void Configure(EntityTypeBuilder<Provider> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.Number).HasMaxLength(50);
            builder.Property(o => o.ContactName).HasMaxLength(200);
            builder.Property(o => o.ContactTitle).HasMaxLength(200);
            builder.Property(o => o.ContactPhone);
            builder.Property(o => o.ContactExt);
            builder.Property(o => o.ContactMobile);
            builder.Property(o => o.ContactEmail).HasMaxLength(500);
            builder.Property(o => o.CompanyWebsite).HasMaxLength(200);
            builder.Property(o => o.RepName).HasMaxLength(500);
            builder.Property(o => o.RepTitle).HasMaxLength(200);
            builder.Property(o => o.RepPhone);
            builder.Property(o => o.RepEmail).HasMaxLength(200);
            builder.Property(o => o.RepSignature);
            builder.Property(o => o.IsPriority);
            builder.Property(o => o.IsNERC);
            builder.HasOne(o => o.ProviderLevel).WithMany(m => m.Providers).HasForeignKey(o => o.ProviderLevelId);
        }
    }
}
