using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILA_DevelopMap : Common.CommonMap<InstructorWorkbook_ILA_Develop>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILA_Develop> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.DevelopResult);
            builder.Property(o => o.InstructorComments);
            builder.Property(o => o.ReviewerComments);
        }

    }
}
