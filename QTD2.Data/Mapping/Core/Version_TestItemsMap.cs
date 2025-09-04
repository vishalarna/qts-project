using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class Version_TestItemsMap : Common.CommonMap<Version_TestItems>
    {
        public override void Configure(EntityTypeBuilder<Version_TestItems> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired().HasMaxLength(200);
            builder.Property(o => o.IsActive).IsRequired();
            builder.HasOne(o => o.TestItem).WithMany(m => m.Version_TestItems).HasForeignKey(k => k.TestItemId).IsRequired().OnDelete(DeleteBehavior.NoAction); ;
            //builder.HasOne(o => o.TestItemType).WithMany(m => m.Version_TestItems).HasForeignKey(k => k.TestItemTypeId).IsRequired();
            //builder.HasOne(o => o.TaxonomyLevel).WithMany(m => m.Version_TestItems).HasForeignKey(k => k.TaxonomyId).IsRequired();
            builder.HasOne(o => o.Version_EnablingObjective).WithMany(m => m.Version_TestItems).HasForeignKey(k => k.Version_EnablingObjectiveId);
        }
    }
}
