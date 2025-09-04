using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ProspectiveILA_ArchivesMap : Common.CommonMap<InstructorWorkbook_ProspectiveILA_Archives>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ProspectiveILA_Archives> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}