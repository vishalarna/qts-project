using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILADesign_TrainingTopicsMap : Common.CommonMap<InstructorWorkbook_ILADesign_TrainingTopics>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILADesign_TrainingTopics> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TrainingTopic).IsRequired();

            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}
