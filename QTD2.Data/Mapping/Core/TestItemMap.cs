using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TestItemMap : Common.CommonMap<TestItem>
    {
        public override void Configure(EntityTypeBuilder<TestItem> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.IsActive).IsRequired();
            builder.HasOne(o => o.TestItemType).WithMany(m => m.TestItems).HasForeignKey(k => k.TestItemTypeId).IsRequired();
            builder.HasOne(o => o.TaxonomyLevel).WithMany(m => m.TestItems).HasForeignKey(k => k.TaxonomyId).IsRequired();
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.TestItems).HasForeignKey(k => k.EOId);
            builder.HasOne(o => o.Image).WithMany().HasForeignKey(k => k.ImageId);
        }
    }
}
