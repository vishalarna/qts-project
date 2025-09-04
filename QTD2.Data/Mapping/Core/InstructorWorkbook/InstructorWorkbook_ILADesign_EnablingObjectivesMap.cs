using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILADesign_EnablingObjectivesMap : Common.CommonMap<InstructorWorkbook_ILADesign_EnablingObjectives>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesign_EnablingObjectives> builder)
        {
            base.Configure(builder);

            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.EnablingObjectivesDescription);
            builder.HasOne(o => o.EnablingObjective).WithMany().HasForeignKey(o => o.EnablingObjectiveId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.ILAObjectiveOrder).IsRequired();
        }

    }
}
