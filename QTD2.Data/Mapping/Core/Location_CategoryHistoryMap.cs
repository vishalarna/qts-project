using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
namespace QTD2.Data.Mapping.Core
{
    public class Location_CategoryHistoryMap : Common.CommonMap<Location_CategoryHistory>
    {
        public override void Configure(EntityTypeBuilder<Location_CategoryHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.Property(o => o.Notes);
            builder.HasOne(o => o.Location_Category).WithMany(x => x.Location_CategoryHistories).HasForeignKey(y => y.LocCategoryID).IsRequired();
        }
    }
}
