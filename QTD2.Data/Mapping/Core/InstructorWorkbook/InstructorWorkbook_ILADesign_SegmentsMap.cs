using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class InstructorWorkbook_ILADesign_SegmentsMap : Common.CommonMap<InstructorWorkbook_ILADesign_Segments>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesign_Segments> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SegmentText).IsRequired();
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.Segment).WithMany().HasForeignKey(o => o.SegmentId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.SegmentTitle).IsRequired();
        }

    }
}
