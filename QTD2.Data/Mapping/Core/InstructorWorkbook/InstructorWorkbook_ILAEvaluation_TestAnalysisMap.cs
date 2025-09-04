using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILAEvaluation_TestAnalysisMap : Common.CommonMap<InstructorWorkbook_ILAEvaluation_TestAnalysis>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILAEvaluation_TestAnalysis> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Notes).IsRequired();
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}
