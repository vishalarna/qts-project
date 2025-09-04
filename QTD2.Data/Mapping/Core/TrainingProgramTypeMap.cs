using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingProgramTypeMap : Common.CommonMap<TrainingProgramType>
    {
        public override void Configure(EntityTypeBuilder<TrainingProgramType> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TrainingProgramTypeTitle).IsRequired().HasMaxLength(200);
        }
    }
}
