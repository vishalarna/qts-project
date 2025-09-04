using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class DifSurveyMap : Common.CommonMap<DIFSurvey>
    {
        public override void Configure(EntityTypeBuilder<DIFSurvey> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.PositionId).IsRequired();
            builder.HasOne(o => o.Position).WithMany(o => o.DIFSurveys).HasForeignKey(k => k.PositionId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(o => o.StartDate).IsRequired();
            builder.Property(o => o.DueDate).IsRequired();
            builder.Property(o => o.Instructions);
            builder.Property(o => o.DevStatusId);
            builder.Property(o => o.ReleasedToEMP);
            builder.Property(o => o.HistoricalOnly);
            builder.HasOne(o => o.DevStatus).WithMany(m => m.DIFSurveys).HasForeignKey(k => k.DevStatusId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}