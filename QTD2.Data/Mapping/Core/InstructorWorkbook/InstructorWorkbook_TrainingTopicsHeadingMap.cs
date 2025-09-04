using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
  public  class InstructorWorkbook_TrainingTopicsHeadingMap : Common.CommonMap<InstructorWorkbook_TrainingTopicsHeading>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_TrainingTopicsHeading> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TrainingTopicHeading).IsRequired();
        }
    }
}
