using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILA_ImplementMap : Common.CommonMap<InstructorWorkbook_ILA_Implement>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILA_Implement> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ImplementResult);
            builder.Property(o => o.InstructorComments);
            builder.Property(o => o.ReviewerComments); 
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}

