using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingProgramMap : Common.CommonMap<TrainingProgram>
    {
        public override void Configure(EntityTypeBuilder<TrainingProgram> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Position).WithMany(tp => tp.TrainingPrograms).HasForeignKey(k => k.PositionId).IsRequired();
            builder.HasOne(o => o.TrainingProgramType).WithMany(tp => tp.TrainingPrograms).HasForeignKey(k => k.TrainingProgramTypeId).IsRequired();
            //builder.Property(o => o.Version).IsRequired();
            builder.Property(o => o.ProgramTitle);
            builder.Property(o => o.StartDate);
            builder.Property(o => o.EndDate);
            builder.Property(o => o.Year);
            builder.Property(o => o.Description);
            builder.HasIndex(i => new { i.PositionId, i.Id }).IsUnique();
            //builder.Navigation(n => n.Position).AutoInclude();
            //builder.Navigation(n => n.TrainingProgramType).AutoInclude();
        }
    }
}
