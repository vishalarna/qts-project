using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class MetaILAMap : Common.CommonMap<MetaILA>
    {
        public override void Configure(EntityTypeBuilder<MetaILA> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(500);
            builder.Property(o => o.Description).HasMaxLength(4000);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.Reason).HasMaxLength(500);
            builder.Property(o => o.IsDeleteAllowed).HasDefaultValue(true);

            builder.HasOne(o => o.MetaILAStatus).WithMany(m => m.MetaILAs).HasForeignKey(o => o.MetaILAStatusId).IsRequired();
            builder.HasOne(o => o.MetaILA_SummaryTest_FinalTest).WithMany().HasForeignKey(o => o.MetaILA_SummaryTest_FinalTestId);
            builder.HasOne(o => o.MetaILA_SummaryTest_RetakeTest).WithMany().HasForeignKey(o => o.MetaILA_SummaryTest_RetakeTestId);
            builder.HasOne(o => o.StudentEvaluation).WithMany().HasForeignKey(o => o.StudentEvaluationId);
            builder.HasOne(o => o.Provider).WithMany(m=>m.MetaILAs).HasForeignKey(o => o.ProviderId);
        }
    }
}
