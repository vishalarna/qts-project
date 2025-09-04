using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILACustomObjective_LinkMap : Common.CommonMap<ILACustomObjective_Link>
    {
        public override void Configure(EntityTypeBuilder<ILACustomObjective_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILACustomObjective_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.CustomEnablingObjective).WithMany(m => m.ILACustomObjective_Links).HasForeignKey(k => k.CustomObjId).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.CustomObjId }).IsUnique().HasFilter("[Deleted] = 0");
        }
    }
}
