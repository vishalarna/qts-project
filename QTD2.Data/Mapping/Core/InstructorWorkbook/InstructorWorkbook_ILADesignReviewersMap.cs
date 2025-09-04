using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILADesignReviewersMap : Common.CommonMap<InstructorWorkbook_ILADesignReviewers>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesignReviewers> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ReviewerIds).IsRequired();

            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        }

    }
}
