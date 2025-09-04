using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingProgram_HistoryMap : Common.CommonMap<TrainingProgram_History>
    {
        public override void Configure(EntityTypeBuilder<TrainingProgram_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TrainingProgramId).IsRequired();
            builder.Property(o => o.TrainingProgramVersionId).IsRequired();

            builder.HasOne(o => o.TrainingProgram).WithMany(x => x.TrainingProgram_Histories).HasForeignKey(y => y.TrainingProgramId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        }
    }
}
