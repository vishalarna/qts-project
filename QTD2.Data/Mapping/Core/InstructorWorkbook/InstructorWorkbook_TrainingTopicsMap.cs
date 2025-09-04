using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
  public  class InstructorWorkbook_TrainingTopicsMap : Common.CommonMap<InstructorWorkbook_TrainingTopics>
  {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_TrainingTopics> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TrainingTopic).IsRequired();
            builder.HasOne(o => o.InstructorWorkbook_TrainingTopicsHeading).WithMany().HasForeignKey(o => o.TTHID).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }
  }
}
