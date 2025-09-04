using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class MetaILA_SummaryTestMap : Common.CommonMap<MetaILA_SummaryTest>
    {
        public override void Configure(EntityTypeBuilder<MetaILA_SummaryTest> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TestInstruction);
            builder.Property(o => o.TestTimeLimitHours);
            builder.Property(o => o.TestTimeLimitMinutes);

            builder.HasOne(o => o.Test).WithMany().HasForeignKey(k => k.TestId).IsRequired();
            builder.HasOne(o => o.TestType).WithMany().HasForeignKey(k => k.TestTypeId).IsRequired();
            builder.HasOne(o => o.Position).WithMany().HasForeignKey(k => k.PositionId);
        }
    }
}
