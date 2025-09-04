using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ProspectiveILA_ReviewersMap : Common.CommonMap<InstructorWorkbook_ProspectiveILA_Reviewers>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ProspectiveILA_Reviewers> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ProspectiveILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.Reviewer).WithMany().HasForeignKey(o => o.ReviewerId).IsRequired();
        }

    }
}
