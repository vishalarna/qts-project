using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILADesign_NERCMap : Common.CommonMap<InstructorWorkbook_ILADesign_NERC>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesign_NERC> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.NercStandard).WithMany().HasForeignKey(o => o.NSID).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}
