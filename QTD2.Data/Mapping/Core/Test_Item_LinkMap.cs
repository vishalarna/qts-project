using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Test_Item_LinkMap : Common.CommonMap<Test_Item_Link>
    {
        public override void Configure(EntityTypeBuilder<Test_Item_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Test).WithMany(m => m.Test_Item_Links).HasForeignKey(k => k.TestId).IsRequired();
            builder.HasOne(o => o.TestItem).WithMany(m => m.Test_Item_Links).HasForeignKey(k => k.TestItemId).IsRequired();
            builder.HasIndex(i => new { i.TestId, i.TestItemId }).IsUnique();
        }
    }
}
