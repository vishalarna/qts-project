using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILADesign_ResourcesMap : Common.CommonMap<InstructorWorkbook_ILADesign_Resources>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesign_Resources> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ResourceName);
            builder.Property(o => o.ResourcePath);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.ILA_Upload).WithMany().HasForeignKey(o => o.ILA_UploadId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}
