using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;


namespace QTD2.Data.Mapping.Core
{
    public class TrainingPrograms_ILA_LinkMap : Common.CommonMap<TrainingPrograms_ILA_Link>
    {
        public override void Configure(EntityTypeBuilder<TrainingPrograms_ILA_Link> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TrainingProgramId).IsRequired();
            builder.Property(o => o.ILAId).IsRequired();
            builder.HasOne(o => o.TrainingProgram).WithMany(x => x.TrainingProgram_ILA_Links).HasForeignKey(y => y.TrainingProgramId).IsRequired();

        }
    }
}
