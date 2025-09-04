using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
  public  class InstructorWorkbook_ILADesign_DelieveryMethodsMap : Common.CommonMap<InstructorWorkbook_ILADesign_DelieveryMethods>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesign_DelieveryMethods> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.InstructorEmail);

            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.DeliveryMethod).WithMany().HasForeignKey(o => o.MID).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}
