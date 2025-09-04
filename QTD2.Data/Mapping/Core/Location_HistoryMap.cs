using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Location_HistoryMap : Common.CommonMap<Location_History>
    {
        public override void Configure(EntityTypeBuilder<Location_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.HasOne(o => o.Location).WithMany(x => x.Location_Histories).HasForeignKey(y => y.LocationId).IsRequired();
        }
    }
}
