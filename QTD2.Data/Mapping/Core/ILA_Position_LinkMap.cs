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
    public class ILA_Position_LinkMap : Common.CommonMap<ILA_Position_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_Position_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_Position_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.Position).WithMany(m => m.ILA_Position_Links).HasForeignKey(k => k.PositionId).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.PositionId }).IsUnique().HasFilter("[Deleted] = 0");
        }
    }
}
