using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QTD2.Data.Mapping.Core
{
    public class DIFSurvey_Task_TrainingFrequencyMap : Common.CommonMap<DIFSurvey_Task_TrainingFrequency>
    {
        public override void Configure(EntityTypeBuilder<DIFSurvey_Task_TrainingFrequency> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Status).IsRequired();
        }
    }
}