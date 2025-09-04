using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILADesign_TasksMap : Common.CommonMap<InstructorWorkbook_ILADesign_Tasks>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesign_Tasks> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.Task).WithMany().HasForeignKey(o => o.TaskId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.ILAObjectiveOrder).IsRequired();
        }

    }
}
