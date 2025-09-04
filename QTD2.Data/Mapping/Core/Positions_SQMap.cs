using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;


namespace QTD2.Data.Mapping.Core
{
    public class Positions_SQMap : Common.CommonMap<Positions_SQ>
    {
        public override void Configure(EntityTypeBuilder<Positions_SQ> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Position).WithMany(x => x.Position_SQs).HasForeignKey(y => y.PositionId).IsRequired();
            builder.HasOne(o => o.EnablingObjective).WithMany(x => x.Position_SQs).HasForeignKey(y => y.EOId).IsRequired();
        }
    }
}
