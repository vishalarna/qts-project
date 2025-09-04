using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILADesign_TargetAudienceMap : Common.CommonMap<InstructorWorkbook_ILADesign_TargetAudience>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesign_TargetAudience> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.InstructorEmail);

            builder.HasOne(o => o.NERCTargetAudience).WithMany().HasForeignKey(o => o.TargetAudienceId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}
